using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    class CircleAbility : MonoBehaviour, IAbility
    {

        [field: SerializeField] public Transform AbilityParent { get; private set; }

        public void Execute()
        {

            var bodies = Physics.OverlapSphere(AbilityParent.position, 10f, AbilitiesConfig.EnemiesLayerMask);



        }

      
    }
}
