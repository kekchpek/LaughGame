using System;
using System.Collections.Generic;
using AsyncReactAwait.Bindable;

namespace LaughGame.GameResources.Container
{
    public interface IResourcesContainer
    {
        event Action<ResourceId, float> ResourceChanged;
        IReadOnlyList<ResourceId> GetNonZeroResources();
        IBindable<float> GetResource(ResourceId resourceId);
    }
}