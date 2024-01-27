using LaughGame.Assets.Scripts.Model.Abilities;
using UnityEngine;

namespace LaughGame.Interaction.Npc
{
    public class NpcDamage : MonoBehaviour, IHealth
    {

        [SerializeField]
        private NpcController _npcController;
        
        public void TakeDamage(float amount)
        {
            _npcController.TakeDamage(amount);
        }
    }
}