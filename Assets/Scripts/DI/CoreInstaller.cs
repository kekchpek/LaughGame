using LaughGame.Assets.Scripts.Model.Abilities;
using LaughGame.GameResources;
using LaughGame.Interaction.Npc;
using LaughGame.Model.Abilities;
using LaughGame.Model.HapinessManager;
using Zenject;
using UnityEngine;

namespace LaughGame.DI
{
    public class CoreInstaller : MonoInstaller
    {

        [SerializeField] private AbilitiesEntitiesProvider _abilitiesEntitiesProvider;

        public override void InstallBindings()
        {
            Container.Install<GameResourcesInstaller>();
            Container.Bind<IPlayerDamageReceiver>().To<PlayerDamageReceiver>().AsSingle();
            Container.Bind<IHappinessManager>().To<HappinessManager>().AsSingle();
            Container.Bind<IAbilitiesEntitiesProvider>().To<AbilitiesEntitiesProvider>().FromInstance(_abilitiesEntitiesProvider);

        }
    }
}