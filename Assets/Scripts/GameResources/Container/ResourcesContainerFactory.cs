using Zenject;

namespace LaughGame.GameResources.Container
{
    public class ResourcesContainerFactory : IFactory<IMutableResourcesContainer>
    {
        private readonly IInstantiator _instantiator;

        public ResourcesContainerFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public IMutableResourcesContainer Create()
        {
            return _instantiator.Instantiate<ResourcesContainer>();
        }
    }
}