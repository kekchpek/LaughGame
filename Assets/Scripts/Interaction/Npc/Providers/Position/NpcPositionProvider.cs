using UnityEngine;

namespace LaughGame.Interaction.Npc
{
    public class NpcPositionProvider : INpcPositionProvider
    {

        private INpcPositionSource _npcPositionSource;
        
        public void SetSource(INpcPositionSource npcPositionSource)
        {
            _npcPositionSource = npcPositionSource;
        }

        public Vector3? GetPosition()
        {
            return _npcPositionSource?.Position;
        }
    }
}