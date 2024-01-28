using LaughGame.Assets.Scripts.Model.Abilities.Stats;
using LaughGame.Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    class CircleAbility : BaseAbility<CircleAbilityStats>
    {
        [SerializeField] private LayerMask _enemyMask;


        public override string AnimationName => "Microphone";
        public override string UpgradeDescription => "+radius";
        
        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("Abilities/Circle");
        }

        public override void Execute()
        {

            var colliders = Physics2D.OverlapCircleAll(
                AbilityParent.MovableTransform.position,
                _curStat.Radius,
                AbilitiesConfig.EnemiesLayerMask);

            Debug.Log($"colliders: {colliders.Length}");

            foreach (var collider in colliders)
            {
                var health = collider.GetComponent<IHealth>();
                if (health != null)
                    health.TakeDamage(_curStat.Damage);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false || AbilityParent == null)
                return;
            UnityEditor.Handles.DrawWireDisc(AbilityParent.MovableTransform.position, Vector3.forward, _curStat.Radius);
        }
#endif

    }
}
