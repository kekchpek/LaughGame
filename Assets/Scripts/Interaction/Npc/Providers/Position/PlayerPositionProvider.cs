using UnityEngine;

namespace LaughGame.Interaction.Npc
{
    public class PlayerPositionProvider : IPlayerPositionProvider
    {

        private IPlayerPositionSource _playerPositionSource;
        
        public void SetSource(IPlayerPositionSource playerPositionSource)
        {
            _playerPositionSource = playerPositionSource;
        }

        public Vector3? GetPosition()
        {
            return _playerPositionSource?.Position;
        }
    }
}