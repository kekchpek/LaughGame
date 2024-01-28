using System.Collections;
using System.Collections.Generic;
using LaughGame.Assets.Scripts.Model.Abilities.Stats;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class LineAbility : BaseAbility<LineAbilityStats>
    {

        [SerializeField]
        private GameObject _banana;
        
        public override string AnimationName => "Banana";
        public override string UpgradeDescription => "+distance";
        
        private Coroutine _routine;
        private float _coroutineTime;
        private HashSet<IHealth> _touchedEnemies = new();

        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("Abilities/Line");
        }

        public override void Execute()
        {
            _banana.SetActive(true);
            if (_routine != null)
                StopCoroutine(_routine);

            _routine = StartCoroutine(StartSequence());
        }

        public IEnumerator StartSequence()
        {
            print($"start coro");
            _coroutineTime = 0;
            _touchedEnemies.Clear();
            AbilityParent.SelfMovementEnabled = false;
            while (_coroutineTime < _curStat.Duration)
            {
                print($"coro move");
                _coroutineTime += Time.fixedDeltaTime;

                Vector2 velocity = AbilityParent.FacingDirection * _curStat.Speed;
                AbilityParent.Move(velocity);

                var colliders = Physics2D.OverlapCircleAll(
                AbilityParent.MovableTransform.position,
                _curStat.HitBoxRadius,
                AbilitiesConfig.EnemiesLayerMask);
                Debug.Log($"colliders: {colliders.Length}");

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
            AbilityParent.SelfMovementEnabled = true;
            _banana.SetActive(false);
            _routine = null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false || AbilityParent == null)
                return;
            UnityEditor.Handles.DrawWireDisc(AbilityParent.MovableTransform.position, Vector3.forward, _curStat.HitBoxRadius);
        }
#endif


    }
}