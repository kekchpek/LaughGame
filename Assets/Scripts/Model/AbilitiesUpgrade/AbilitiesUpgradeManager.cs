using System.Linq;
using System.Threading.Tasks;
using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;
using LaughGame.Model.Abilities.AbilitiesRegister;
using UnityEngine;

namespace LaughGame.Model.AbilitiesUpgrade
{
    public class AbilitiesUpgradeManager : IAbilitiesUpgradeManager
    {
        private readonly IAbilitiesRegister _abilitiesRegister;

        private IAbilitiesPopup _abilitiesPopup;

        public AbilitiesUpgradeManager(
            IAbilitiesRegister abilitiesRegister)
        {
            _abilitiesRegister = abilitiesRegister;
        }

        public void SetPopup(IAbilitiesPopup abilitiesPopup)
        {
            _abilitiesPopup = abilitiesPopup;
        }
        
        private IAbility[] GetAbilitiesToUpgrade()
        {
            var availableAbilities = _abilitiesRegister.GetAbilities()
                .Where(x => x.CanUpgrade)
                .ToArray();
            if (availableAbilities.Length <= 2)
                return availableAbilities;
            var rand = Random.Range(0, availableAbilities.Length);
            return new[] {
                availableAbilities[rand],
                availableAbilities[(rand + 1) % availableAbilities.Length] };
        }

        public async Task StartUpgrade()
        {
            Time.timeScale = 0f;
            await _abilitiesPopup.Upgrade(GetAbilitiesToUpgrade());
            Time.timeScale = 1f;
        }
    }
}