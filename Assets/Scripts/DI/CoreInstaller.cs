using LaughGame.GameResources;
using LaughGame.Interaction.Npc;
using Zenject;

namespace LaughGame.DI
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameResourcesInstaller>();
            Container.Bind<IDamageReceiver>().To<DamageReceiver>().AsSingle();
        }
    }
}