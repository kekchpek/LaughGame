using UnityEngine;

namespace LaughGame.Interaction.Npc
{
    public interface INpcPositionProvider
    {
        void SetSource(INpcPositionSource npcPositionSource);
        
        Vector3? GetPosition();
    }
}