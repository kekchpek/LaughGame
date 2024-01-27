using LaughGame.GameResources;
using Zenject;

namespace LaughGame.DI
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameResourcesInstaller>();
        }
    }
}