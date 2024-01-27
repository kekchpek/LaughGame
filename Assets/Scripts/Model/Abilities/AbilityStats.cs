using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    [System.Serializable]
    public class AbilityStats
    {
        public float Damage;
        public float Radius;
        public float Speed;
        public float Distance;
        public float HitBoxRadius;
        public float Duration => Distance / Speed;

    }
}
