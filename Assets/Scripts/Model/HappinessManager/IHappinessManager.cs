using AsyncReactAwait.Bindable;

namespace LaughGame.Model.HapinessManager
{
    public interface IHappinessManager
    {
        float MaxHappiness { get; }
        IBindable<float> Happiness { get; }
        void SetHappinessPercent(float happinessPercent);
        void AddHappiness();
        void SubtractHappiness(float val);
    }
}