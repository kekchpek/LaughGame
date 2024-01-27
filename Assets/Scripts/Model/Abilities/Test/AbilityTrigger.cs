using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;
using System.Collections;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities.Test
{
    public class AbilityTrigger : MonoBehaviour
    {

        [SerializeField] private LightningAbility _abilityToTrigger;



        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                _abilityToTrigger.Execute();
            }
        }
    }
}