using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;
using LaughGame.Model.Abilities;
using System.Collections.Generic;
using LaughGame.Model.Abilities.AbilitiesRegister;
using UnityEngine;
using Zenject;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public abstract class BaseAbility<T> : MonoBehaviour, IAbility
    {

        [SerializeField] private List<T> _stats;
        [SerializeField] private IAbilitiesEntitiesProvider _entitiesProvider;

        protected T _curStat;


        private int _statIndex = 0;

        public IMovable AbilityParent => _entitiesProvider.GetMovablePlayer();

        

        [Inject]
        public void Construct(
            IAbilitiesEntitiesProvider entitiesProvider,
            IAbilitiesRegister abilitiesRegister)
        {
            abilitiesRegister.Register(this);
            _entitiesProvider = entitiesProvider;
        }

        private void Start()
        {
            _curStat = _stats[0];
        }

        public abstract Sprite GetSprite();
        
        public abstract void Execute();
        public void Upgrade()
        {
            if (CanUpgrade() == false)
                return;
            _statIndex++;

            _curStat = _stats[_statIndex];
        }

        public bool CanUpgrade()
        {
            return _statIndex < _stats.Count;
        }
    }
}
