using System;
using LaughGame.Interaction.Npc;
using LaughGame.Model.Abilities;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.Arrow
{
    public class ArrowController : MonoBehaviour
    {

        private IAbilitiesEntitiesProvider _abilitiesEntitiesProvider;

        private Transform _transform;

        [Inject]
        public void Construct(IAbilitiesEntitiesProvider abilitiesEntitiesProvider)
        {
            _transform = transform;
            _abilitiesEntitiesProvider = abilitiesEntitiesProvider;
        }

        private void Update()
        {
            var dir = _abilitiesEntitiesProvider.GetMovablePlayer().FacingDirection;
            _transform.LookAt(_transform.position + Vector3.forward, dir);
        }
    }
}