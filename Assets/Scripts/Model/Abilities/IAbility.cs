﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public interface IAbility
    {

        public  IMovable AbilityParent { get; }
        public void Execute();
    }
}