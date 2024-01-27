using LaughGame.Assets.Scripts.Model.Abilities.Stats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class LightningAbility : BaseAbility<LightningAbilityStats>
    {
        [SerializeField] private LineRenderer _lineRenderer;
        private Coroutine _routine;
        private HashSet<Collider2D> _touchedColliders = new();
        private int _jumpCompleted = 0;

        private Vector3 _lastHitPos;
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
            _lineRenderer.positionCount = 1;
            _lineRenderer.SetPosition(0, AbilityParent.MovableTransform.position);

            while (_jumpCompleted < _curStat.NumberOfJumps)
            {

                var radius = _jumpCompleted == 0 ? _curStat.FirstJumpRadius : _curStat.JumpRadius;

                var colliders = Physics2D.OverlapCircleAll(
                AbilityParent.MovableTransform.position,
                radius,
                AbilitiesConfig.EnemiesLayerMask)
                    .Where(x => _touchedColliders.Contains(x) == false);


                print($"Jump {_jumpCompleted+1}, col count: {colliders.Count()}");
                if (colliders.Count() == 0)
                    break;

                var randomIndex = Random.Range(0, colliders.Count());

                var randomEnemyCollider = colliders.ElementAt(randomIndex);
                _lastHitPos = randomEnemyCollider.transform.position;

                var health = randomEnemyCollider.GetComponent<IHealth>();
                if (health != null)
                {
                    health.TakeDamage(_curStat.Damage);
                    _touchedColliders.Add(randomEnemyCollider);
                }

                _jumpCompleted++;
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_jumpCompleted, randomEnemyCollider.transform.position);

                yield return new WaitForSeconds(_curStat.DelayBetweenJumps);
            }
            _lineRenderer.positionCount = 0;
            _routine = null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false)
                return;
            UnityEditor.Handles.DrawWireDisc(_lastHitPos, Vector3.forward, _curStat.JumpRadius);
        }
#endif
    }
}