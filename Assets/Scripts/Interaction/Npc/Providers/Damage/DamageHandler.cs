using System;
using LaughGame.Model.HapinessManager;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.Npc
{
    public class DamageHandler : MonoBehaviour, IPlayerDamagable
    {

        private IHappinessManager _happinessManager;
        private IPlayerDamageReceiver _playerDamageReceiver;

        [Inject]
        public void Construct(
            IHappinessManager happinessManager,
            IPlayerDamageReceiver playerDamageReceiver)
        {
            _happinessManager = happinessManager;
            _playerDamageReceiver = playerDamageReceiver;
            _playerDamageReceiver.SetDamagable(this);
        }
        
        public void TakeDamage(float damage)
        {
            _happinessManager.SubtractHappiness(damage);
        }

        private void OnDestroy()
        {
            _playerDamageReceiver.SetDamagable(null);
        }
    }
}