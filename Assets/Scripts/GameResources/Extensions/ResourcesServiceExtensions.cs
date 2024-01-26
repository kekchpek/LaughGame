namespace LaughGame.GameResources.Extensions
{
    public static class ResourcesServiceExtensions
    {
        public static bool TryToSpend(this IResourcesService resourcesService, ResourceId resourceId, float amount)
        {
            return resourcesService.TryToSpend(new[] { (resourceId, amount) });
        }
    }
}