using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;
using LaughGame.GameResources;
using LaughGame.Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public abstract class BaseAbility<T> : MonoBehaviour, IAbility
    {

        [SerializeField] private List<T> _stats;

        protected T _curStat;


        private int _statIndex = 0;

        public IMovable AbilityParent { get; protected set; }
        

        [Inject]
        public void Construct(IAbilitiesEntitiesProvider entitiesProvider)
        {
            AbilityParent = entitiesProvider.GetMovablePlayer();
        }

        private void Start()
        {
            _curStat = _stats[0];
        }

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
