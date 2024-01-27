using System;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.Npc
{
    public class NpcPositionSource : MonoBehaviour, INpcPositionSource
    {
        public Vector3 Position => _transform.position;

        private Transform _transform;

        private INpcPositionProvider _npcPositionProvider;

        private void Awake()
        {
            _transform = transform;
        }

        [Inject]
        public void Construct(INpcPositionProvider npcPositionProvider)
        {
            _npcPositionProvider.SetSource(this);
        }

        private void OnDestroy()
        {
            _npcPositionProvider.SetSource(null);
        }
    }
}