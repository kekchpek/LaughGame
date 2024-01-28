using System;

namespace LaughGame.Model.AbilitiesManagement
{
    public interface IAbilitiesManager
    {
        event Action<int> AbilityUpdated;
        AbilityData? Get(int index);
        bool TryUse(int abilityIndex);
    }
}