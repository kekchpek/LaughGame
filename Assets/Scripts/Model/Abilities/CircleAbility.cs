using LaughGame.Assets.Scripts.Model.Abilities.Stats;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    class CircleAbility : BaseAbility<CircleAbilityStats>
    {
        [SerializeField] private LayerMask _enemyMask;

        [SerializeField]
        private ParticleSystem _particleSystem;


        public override string AnimationName => "Microphone";
        public override string UpgradeDescription => "+radius";
        
        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("Abilities/Circle");
        }

        public override void Execute()
        {
            AudioManager.Play(AudioManager.AudioConfig.Mic);
            var sol = _particleSystem.sizeOverLifetime;
            sol.sizeMultiplier = _curStat.Radius * 2;
            _particleSystem.Play();
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
