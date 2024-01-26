using System.Collections.Generic;

namespace LaughGame.GameResources
{
    public interface IResourcesService
    {
        bool TryToSpend(IEnumerable<(ResourceId resourceId, float amount)> resources);
        void Reduce(ResourceId resourceId, float amount);
        void Add(ResourceId resourceId, float amount);
    }
}