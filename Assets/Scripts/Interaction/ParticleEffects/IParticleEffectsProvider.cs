using UnityEngine;

namespace LaughGame.Interaction.ParticleEffects
{
    public interface IParticleEffectsProvider
    {
        void PlayStars(Vector3 position);
        void PlayDrops(Vector3 position);
    }
}