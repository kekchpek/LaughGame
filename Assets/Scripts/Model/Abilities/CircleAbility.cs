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
    

        [Inject]
        public void Construct(IAbilitiesEntitiesProvider entitiesProvider)
        {
            AbilityParent = entitiesProvider.GetMovablePlayer();
        }

        public override void Execute()
        {

            var colliders = Physics2D.OverlapCircleAll(
                AbilityParent.MovableTransform.position,
                _curStat.Radius,
                AbilitiesConfig.EnemiesLayerMask);

          

            foreach (var collider in colliders)
            {
                var health = collider.GetComponent<IHealth>();
                if (health != null)
                    health.TakeDamage(_curStat.Damage);
            }
        }


        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false)
                return;
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _curStat.Radius);
        }

    }
}
