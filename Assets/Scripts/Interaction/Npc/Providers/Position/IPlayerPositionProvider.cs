using UnityEngine;

namespace LaughGame.Interaction.Npc
{
    public interface IPlayerPositionProvider
    {
        void SetSource(IPlayerPositionSource playerPositionSource);
        
        Vector3? GetPosition();
    }
}