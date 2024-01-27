using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaughGame.Model.Abilities;
using UnityEngine;
using Zenject;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class LineAbility : MonoBehaviour, IAbility
    {
        public IMovable AbilityParent { get; private set; }

        private LineAbilityStats _stats;
        private Coroutine _routine;
        private float _duration;
        private float _coroutineTime;
        private HashSet<IHealth> _touchedEnemies = new();

        [Inject]
        public void Construct(IAbilitiesEntitiesProvider entitiesProvider)
        {
            AbilityParent = entitiesProvider.GetMovablePlayer();
        }

        public void Execute()
        {
            if (_routine != null)
                StopCoroutine(_routine);

            _routine = StartCoroutine(StartSequence());
        }

        public IEnumerator StartSequence()
        {
            while (_coroutineTime < _duration)
            {
                _duration += Time.fixedDeltaTime;

                Vector2 velocity = AbilityParent.MovableTransform.forward * _stats.Speed;
                AbilityParent.Move(velocity);

                var colliders = Physics.OverlapSphere(
                AbilityParent.MovableTransform.position,
                _stats.HitBoxRadius,
                AbilitiesConfig.EnemiesLayerMask);

                foreach (var collider in colliders)
                {
                    var health = collider.GetComponent<IHealth>();
                    if (health != null && _touchedEnemies.Contains(health) == false)
                    {
                        health.TakeDamage(_stats.Damage);
                        _touchedEnemies.Add(health);
                    }
                }

                yield return new WaitForFixedUpdate();
            }
            _routine = null;
        }

        public void ReceiveUpgrade()
        {
            _duration = _stats.Distance / _stats.Speed;
        }
    }
}