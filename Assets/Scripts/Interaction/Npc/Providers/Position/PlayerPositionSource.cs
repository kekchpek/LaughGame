using System;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.Npc
{
    public class PlayerPositionSource : MonoBehaviour, IPlayerPositionSource
    {
        public Vector3 Position => _transform.position;

        private Transform _transform;

        private IPlayerPositionProvider _playerPositionProvider;

        private void Awake()
        {
            _transform = transform;
        }

        [Inject]
        public void Construct(IPlayerPositionProvider playerPositionProvider)
        {
            _playerPositionProvider = playerPositionProvider;
            _playerPositionProvider.SetSource(this);
        }

        private void OnDestroy()
        {
            _playerPositionProvider.SetSource(null);
        }
    }
}