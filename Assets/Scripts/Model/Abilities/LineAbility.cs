using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaughGame.Model.Abilities;
using UnityEngine;
using Zenject;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class LineAbility : BaseAbility
    {

        private Coroutine _routine;
        private float _coroutineTime;
        private HashSet<IHealth> _touchedEnemies = new();

        [Inject]
        public void Construct(IAbilitiesEntitiesProvider entitiesProvider)
        {
            AbilityParent = entitiesProvider.GetMovablePlayer();
            _curStat = _stats[0];
        }

        public override void Execute()
        {
            if (_routine != null)
                StopCoroutine(_routine);

            _routine = StartCoroutine(StartSequence());
        }

        public IEnumerator StartSequence()
        {
            while (_coroutineTime < _curStat.Duration)
            {
                _coroutineTime += Time.fixedDeltaTime;

                Vector2 velocity = AbilityParent.MovableTransform.forward * _curStat.Speed;
                AbilityParent.Move(velocity);

                var colliders = Physics2D.OverlapCircleAll(
                AbilityParent.MovableTransform.position,
                _curStat.HitBoxRadius,
                AbilitiesConfig.EnemiesLayerMask);

                foreach (var collider in colliders)
                {
                    var health = collider.GetComponent<IHealth>();
                    if (health != null && _touchedEnemies.Contains(health) == false)
                    {
                        health.TakeDamage(_curStat.Damage);
                        _touchedEnemies.Add(health);
                    }
                }

                yield return new WaitForFixedUpdate();
            }
            _touchedEnemies.Clear();
            _routine = null;
        }

 
    }
}