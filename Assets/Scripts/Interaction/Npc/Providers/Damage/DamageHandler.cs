using System;
using LaughGame.Interaction.PlayerAnimations;
using LaughGame.Model.HapinessManager;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.Npc
{
    public class DamageHandler : MonoBehaviour, IPlayerDamagable
    {

        private IHappinessManager _happinessManager;
        private IPlayerDamageReceiver _playerDamageReceiver;
        private IPlayerAnimationProvider _playerAnimationProvider;

        [Inject]
        public void Construct(
            IHappinessManager happinessManager,
            IPlayerDamageReceiver playerDamageReceiver,
            IPlayerAnimationProvider playerAnimationProvider)
        {
            _happinessManager = happinessManager;
            _playerDamageReceiver = playerDamageReceiver;
            _playerAnimationProvider = playerAnimationProvider;
            _playerDamageReceiver.SetDamagable(this);
        }
        
        public void TakeDamage(float damage)
        {
            _happinessManager.SubtractHappiness(damage);
            _playerAnimationProvider.PlayDamage();
        }

        private void OnDestroy()
        {
            _playerDamageReceiver.SetDamagable(null);
        }
    }
}