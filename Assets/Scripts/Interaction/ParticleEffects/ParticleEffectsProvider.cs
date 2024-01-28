using UnityEngine;

namespace LaughGame.Interaction.ParticleEffects
{
    public class ParticleEffectsProvider : MonoBehaviour, IParticleEffectsProvider
    {

        [SerializeField]
        private ParticleSystem _dropsParticles;

        [SerializeField]
        private ParticleSystem _starsParticles;
        
        public void PlayStars(Vector3 position)
        {
            PlayParticles(_starsParticles, position);
        }

        public void PlayDrops(Vector3 position)
        {
            PlayParticles(_dropsParticles, position);
        }

        private void PlayParticles(ParticleSystem particles, Vector3 position)
        {
            particles.transform.position = position;
            particles.Play();
        }
    }
}