using System;
using System.Linq;
using LaughGame.GameResources;
using LaughGame.Model.Abilities.AbilitiesRegister;
using Random = UnityEngine.Random;

namespace LaughGame.Model.AbilitiesManagement
{
    public class AbilityManager : IAbilitiesManager, IDisposable
    {
        private readonly IAbilitiesRegister _abilitiesRegister;
        private readonly IResourcesService _resourcesService;

        private static readonly ResourceId?[] RandomResources = ResourceId.All
            .Cast<ResourceId?>()
            .Append(null)
            .ToArray();

        public event Action<int> AbilityUpdated;
        
        private readonly AbilityData[] _abilities = new AbilityData[4];

        public AbilityManager(
            IAbilitiesRegister abilitiesRegister,
            IResourcesService resourcesService)
        {
            _abilitiesRegister = abilitiesRegister;
            _resourcesService = resourcesService;
            _abilitiesRegister.Inited.Bind(OnInited);
        }

        private void OnInited(bool val)
        {
            if (!val)
                return;
            _abilitiesRegister.Inited.Unbind(OnInited);
            for (int i = 0; i < _abilities.Length; i++)
            {
                _abilities[i] = GetRandom();
                AbilityUpdated?.Invoke(i);
            }
        }

        public AbilityData Get(int index)
        {
            return _abilities[index];
        }

        public bool TryUse(int abilityIndex)
        {
            var abilityData = _abilities[abilityIndex];
            var price = abilityData.Price
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .GroupBy(x => x)
                .Select(x => (x.Key, (float)x.Count()));
            if (_resourcesService.TryToSpend(price))
            {
                abilityData.Ability.Execute();
                _abilities[abilityIndex] = GetRandom();
                AbilityUpdated?.Invoke(abilityIndex);
                return true;
            }

            return false;
        }

        private AbilityData GetRandom()
        {
            var price = new ResourceId?[3];
            for (int i = 0; i < price.Length; i++)
            {
                var rand = Random.Range(0, RandomResources.Length);
                price[i] = RandomResources[rand];
            }

            if (price.All(x => !x.HasValue))
            {
                var rand = Random.Range(0, ResourceId.All.Count);
                price[0] = ResourceId.All[rand];
            }

            var abilities = _abilitiesRegister.GetAbilities();
            var randAbilityIndex = Random.Range(0, abilities.Count);
            
            return new AbilityData(abilities[randAbilityIndex], price);
        }

        public void Dispose()
        {
            _abilitiesRegister.Inited.Unbind(OnInited);
        }
    }
}