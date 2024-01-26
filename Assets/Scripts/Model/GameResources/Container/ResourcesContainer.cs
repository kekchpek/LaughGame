using System;
using System.Collections.Generic;
using System.Linq;
using AsyncReactAwait.Bindable;
using AsyncReactAwait.Bindable.BindableExtensions;

namespace LaughGame.GameResources.Container
{
    public class ResourcesContainer : IMutableResourcesContainer
    {

        public event Action<ResourceId, float> ResourceChanged;

        private readonly Dictionary<ResourceId, IMutable<float>> _resources = new();

        public Dictionary<ResourceId, IMutable<float>> Resources => _resources;

        public IReadOnlyList<ResourceId> GetNonZeroResources()
        {
            return Resources.Keys.Where(k => Resources[k].Value > 0d).ToArray();
        }
        
        public IBindable<float> GetResource(ResourceId resourceId)
        {
            AddResourceIfNeeded(resourceId);
            return _resources[resourceId];
        }

        public void SetResource(ResourceId resourceId, float value)
        {
            AddResourceIfNeeded(resourceId);
            var bindable = _resources[resourceId];
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (bindable.Value == value)
                return;
            _resources[resourceId].Set(value);
            ResourceChanged?.Invoke(resourceId, value);
            
        }

        private void AddResourceIfNeeded(ResourceId id)
        {
            if (!_resources.ContainsKey(id))
            {
                _resources.Add(id, new Mutable<float>());
            }
        }
    }
}