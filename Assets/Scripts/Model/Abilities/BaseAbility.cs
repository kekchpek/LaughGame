using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public abstract class BaseAbility<T> : MonoBehaviour
    {

        [SerializeField] protected List<T> _stats;

        protected T _curStat;
        protected int _statIndex = 0;

        public  IMovable AbilityParent { get; protected set; }
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
