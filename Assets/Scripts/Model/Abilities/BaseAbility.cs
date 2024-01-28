using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;
using LaughGame.Model.Abilities;
using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using Finespace.LofiLegends.MVVM.Models.Audio;
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


        private IMutable<int> _statIndex = new Mutable<int>();
        private bool _canUpgrade;

        public IMovable AbilityParent => _entitiesProvider?.GetMovablePlayer();

        protected IAudioManager AudioManager;
        
        [Inject]
        public void Construct(
            IAbilitiesEntitiesProvider entitiesProvider,
            IAbilitiesRegister abilitiesRegister,
            IAudioManager audioManager)
        {
            abilitiesRegister.Register(this);
            _entitiesProvider = entitiesProvider;
            AudioManager = audioManager;
        }

        private void Start()
        {
            _curStat = _stats[0];
        }

        public abstract Sprite GetSprite();
        
        public abstract void Execute();
        public void Upgrade()
        {
            if (CanUpgrade == false)
                return;
            _statIndex.Value++;

            _curStat = _stats[_statIndex.Value];
        }

        public abstract string AnimationName { get; }
        public abstract string UpgradeDescription { get; }
        public IBindable<int> CurrentLevel => _statIndex;
        public bool CanUpgrade => _statIndex.Value < _stats.Count - 1;
    }
}
