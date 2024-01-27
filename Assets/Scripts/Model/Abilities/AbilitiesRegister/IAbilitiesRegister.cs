using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;

namespace LaughGame.Model.Abilities.AbilitiesRegister
{
    public interface IAbilitiesRegister
    {
        IBindable<bool> Inited { get; }
        IReadOnlyList<IAbility> GetAbilities();
        void Register(IAbility ability);
    }
}