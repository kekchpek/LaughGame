using LaughGame.Assets.Scripts.Model.Abilities.Stats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class LightningAbility : BaseAbility<LightningAbilityStats>
    {

        private Coroutine _routine;
        private HashSet<Collider2D> _touchedColliders = new();
        private int _jumpCompleted = 0;


        public override void Execute()
        {
            if (_routine != null)
                StopCoroutine(_routine);

            _routine = StartCoroutine(StartSequence());
        }

        public IEnumerator StartSequence()
        {
            _jumpCompleted = 0;
            _touchedColliders.Clear();

            while (_jumpCompleted < _curStat.NumberOfJumps)
            {

                var radius = _jumpCompleted == 0 ? _curStat.FirstJumpRadius : _curStat.JumpRadius;

                var colliders = Physics2D.OverlapCircleAll(
                AbilityParent.MovableTransform.position,
                radius,
                AbilitiesConfig.EnemiesLayerMask)
                    .Where(x => _touchedColliders.Contains(x) == false);

                var randomIndex = Random.Range(0, colliders.Count());

                var randomEnemyCollider = colliders.ElementAt(randomIndex);

                var health = randomEnemyCollider.GetComponent<IHealth>();
                if (health != null)
                {
                    health.TakeDamage(_curStat.Damage);
                    _touchedColliders.Add(randomEnemyCollider);
                }

                _jumpCompleted++;
                yield return new WaitForSeconds(_curStat.DelayBetweenJumps);
            }
            _routine = null;
        }


    }
}