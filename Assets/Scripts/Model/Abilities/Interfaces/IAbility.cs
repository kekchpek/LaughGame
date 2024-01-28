using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities.Interfaces
{
    public interface IAbility
    {
        string AnimationName { get; }
        string UpgradeDescription { get; }
        int CurrentLevel { get; }
        bool CanUpgrade { get; }
        Sprite GetSprite();
        void Execute();
        void Upgrade();
    }
}
