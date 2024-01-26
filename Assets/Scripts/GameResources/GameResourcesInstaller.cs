using LaughGame.GameResources.Container;
using Zenject;

namespace LaughGame.GameResources
{
    public class GameResourcesInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(IResourcesMutableModel),
                typeof(IResourcesModel)).To<ResourcesModel>().AsSingle();
            Container.Bind<IResourcesService>().To<ResourcesService>().AsSingle();
            Container.Bind<IFactory<IMutableResourcesContainer>>().To<ResourcesContainerFactory>().AsSingle();
        }
    }
}