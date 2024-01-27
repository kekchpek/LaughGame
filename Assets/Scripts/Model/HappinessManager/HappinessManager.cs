using AsyncReactAwait.Bindable;
using LaughGame.Model.AbilitiesUpgrade;

namespace LaughGame.Model.HapinessManager
{
    public class HappinessManager : IHappinessManager
    {
        private readonly IAbilitiesUpgradeManager _upgradeManager;
        private const float MaxHappinessValue = 100f;
        private const float HappinessBonus = 2f;

        private readonly IMutable<float> _happiness = new Mutable<float>(50f);

        public float MaxHappiness => MaxHappinessValue;
        public IBindable<float> Happiness => _happiness;

        public HappinessManager(IAbilitiesUpgradeManager upgradeManager)
        {
            _upgradeManager = upgradeManager;
        }

        public void SetHappinessPercent(float happinessPercent)
        {
            _happiness.Value = MaxHappiness * happinessPercent;
        }

        public async void AddHappiness()
        {
            _happiness.Value += HappinessBonus;
            while (_happiness.Value > MaxHappinessValue)
            {
                await _upgradeManager.StartUpgrade();
                _happiness.Value -= MaxHappinessValue;
            }
        }

        public void SubtractHappiness(float val)
        {
            _happiness.Value -= val;
            if (_happiness.Value < 0f)
            {
                // Lose
            }
        }
    }
}