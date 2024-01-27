using LaughGame.Model.Abilities;
using System.Collections;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public class AbilitiesEntitiesProvider : MonoBehaviour, IAbilitiesEntitiesProvider
    {
        [SerializeField] private GameObject _player;
        private IMovable _movablePlayer;

        public IMovable GetMovablePlayer()
        {
            if(_movablePlayer == null)
                _movablePlayer = _player.GetComponent<IMovable>();

            return _movablePlayer;
        }


    }
}