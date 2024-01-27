using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    public interface IHealth
    {

        void TakeDamage(float amount);
        void Die();

        

    }
}
