using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;

namespace LaughGame.Model.Abilities.AbilitiesRegister
{
    public class AbilitiesRegister : IAbilitiesRegister
    {

        private readonly List<IAbility> _abilities = new();
        private readonly IMutable<bool> _inited = new Mutable<bool>();

        public IBindable<bool> Inited => _inited;

        public IReadOnlyList<IAbility> GetAbilities()
        {
            return _abilities;
        }

        public void Register(IAbility ability)
        {
            _abilities.Add(ability);
            _inited.Value = _abilities.Count == 4;
        }
    }
}