using AsyncReactAwait.Bindable;

namespace LaughGame.Model.HapinessManager
{
    public class HappinessManager : IHappinessManager
    {
        private const float MaxHappinessValue = 100f;
        private const float HappinessBonus = 1f;

        private readonly IMutable<float> _happiness = new Mutable<float>(50f);

        public float MaxHappiness => MaxHappinessValue;
        public IBindable<float> Happiness => _happiness;

        public void SetHappinessPercent(float happinessPercent)
        {
            _happiness.Value = MaxHappiness * happinessPercent;
        }

        public void AddHappiness()
        {
            _happiness.Value += HappinessBonus;
            if (_happiness.Value > MaxHappinessValue)
            {
                // add level
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