using LaughGame.Assets.Scripts.Model.Abilities;
using LaughGame.GameResources;
using LaughGame.Interaction.Npc;
using LaughGame.Model.Abilities;
using LaughGame.Model.Abilities.AbilitiesRegister;
using LaughGame.Model.AbilitiesManagement;
using LaughGame.Model.HapinessManager;
using Zenject;
using UnityEngine;

namespace LaughGame.DI
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<GameResourcesInstaller>();
            Container.Bind<IPlayerDamageReceiver>().To<PlayerDamageReceiver>().AsSingle();
            Container.Bind<IHappinessManager>().To<HappinessManager>().AsSingle();
            Container.Bind<IAbilitiesEntitiesProvider>().To<AbilitiesEntitiesProvider>().AsSingle();
            Container.Bind<IPlayerPositionProvider>().To<PlayerPositionProvider>().AsSingle();
            Container.Bind<IAbilitiesRegister>().To<AbilitiesRegister>().AsSingle();
            Container.Bind<IAbilitiesManager>().To<AbilityManager>().AsSingle();
        }
    }
}