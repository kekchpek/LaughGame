using System.Collections.Generic;
using System.Linq;
using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;
using LaughGame.GameResources;

namespace LaughGame.Model.AbilitiesManagement
{
    public struct AbilityData
    {
        public readonly ResourceId?[] Price;
        public readonly IAbility Ability;

        public AbilityData(IAbility ability, IEnumerable<ResourceId?> price)
        {
            Ability = ability;
            Price = price.ToArray();
        }
    }
}