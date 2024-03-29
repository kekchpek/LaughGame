﻿using LaughGame.Assets.Scripts.Model.Abilities.Stats;
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
        public override string AnimationName => "Gun";
        public override string UpgradeDescription => "+angle";

        [SerializeField]
        private ParticleSystem _particleSystem;
        
        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("Abilities/Cone");
        }

        public override void Execute()
        {
            AudioManager.Play(AudioManager.AudioConfig.Gun);
            var enemies = GetEnemies();
            
            _particleSystem.Play();
            var shape = _particleSystem.shape;
            shape.angle = _curStat.ConeAngle;
            var emission = _particleSystem.emission;
            emission.rateOverTimeMultiplier = 2000f * (_curStat.ConeAngle / 30f) * (_curStat.ConeAngle / 30f);
            _particleSystem.transform.LookAt(transform.position + (Vector3)AbilityParent.FacingDirection);

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
                Vector2 directionToEnemy = collider.transform.position + (Vector3)collider.offset - AbilityParent.MovableTransform.position;
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
            if (Application.isPlaying == false || AbilityParent == null)
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
