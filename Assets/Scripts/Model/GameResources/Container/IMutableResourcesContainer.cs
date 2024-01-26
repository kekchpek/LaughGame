namespace LaughGame.GameResources.Container
{
    public interface IMutableResourcesContainer : IResourcesContainer
    {
        void SetResource(ResourceId resourceId, float value);
    }
}