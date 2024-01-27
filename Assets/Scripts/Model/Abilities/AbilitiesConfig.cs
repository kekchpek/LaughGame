using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    static class AbilitiesConfig
    {
        public static readonly int EnemiesLayerMask = 1 << LayerMask.NameToLayer("Npc");
    }
}
