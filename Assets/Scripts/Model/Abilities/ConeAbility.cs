using LaughGame.Assets.Scripts.Model.Abilities.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class ConeAbility : BaseAbility<ConeAbilityStats>
    {
        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("Abilities/Cone");
        }

        public override void Execute()
        {
            var enemies = GetEnemies();

            foreach (var enemy in enemies)
            {
                enemy.TakeDamage(_curStat.Damage);
            }
        }

        private HashSet<IHealth> GetEnemies()
        {
            HashSet<IHealth> enemies = new();

            var colliders = Physics2D.OverlapCircleAll(
                AbilityParent.MovableTransform.position,
                _curStat.Length,
                AbilitiesConfig.EnemiesLayerMask);

            foreach (var collider in colliders)
            {
                Vector2 directionToEnemy = collider.transform.position - AbilityParent.MovableTransform.position;
                float angle = Vector2.Angle(AbilityParent.FacingDirection, directionToEnemy);

                if (angle > _curStat.ConeAngle)
                    continue;


                var health = collider.GetComponent<IHealth>();
                if (health != null)
                    enemies.Add(health);
            }


            return enemies;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (Application.isPlaying == false)
                return;


            //UnityEditor.Handles.DrawWireDisc(AbilityParent.MovableTransform.position, Vector3.forward, _curStat.Length);
            UnityEditor.Handles.DrawWireArc(
                AbilityParent.MovableTransform.position,
                Vector3.forward,
                AbilityParent.FacingDirection.Rotate(-_curStat.ConeAngle),
                _curStat.ConeAngle*2,
                _curStat.Length);
        }
#endif
    }
}
