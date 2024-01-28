using Finespace.LofiLegends.MVVM.Models.Audio;
using LaughGame.Assets.Scripts.Model.Abilities;
using LaughGame.GameResources;
using LaughGame.Interaction.Boss;
using LaughGame.Interaction.Npc;
using LaughGame.Interaction.ParticleEffects;
using LaughGame.Interaction.PlayerAnimations;
using LaughGame.Model.Abilities;
using LaughGame.Model.Abilities.AbilitiesRegister;
using LaughGame.Model.AbilitiesManagement;
using LaughGame.Model.AbilitiesUpgrade;
using LaughGame.Model.HapinessManager;
using Zenject;
using UnityEngine;

namespace LaughGame.DI
{
    public class CoreInstaller : MonoInstaller
    {

        [SerializeField]
        private ParticleEffectsProvider _particleEffectsProvider;
        [SerializeField]
        private BossSpawner _bossSpawner;
        [SerializeField]
        private AudioManager _audioManager;
        
        public override void InstallBindings()
        {
            Container.Install<GameResourcesInstaller>();
            Container.Bind<IPlayerDamageReceiver>().To<PlayerDamageReceiver>().AsSingle();
            Container.Bind<IHappinessManager>().To<HappinessManager>().AsSingle();
            Container.Bind<IAbilitiesEntitiesProvider>().To<AbilitiesEntitiesProvider>().AsSingle();
            Container.Bind<IPlayerPositionProvider>().To<PlayerPositionProvider>().AsSingle();
            Container.Bind<IAbilitiesRegister>().To<AbilitiesRegister>().AsSingle();
            Container.Bind<IAbilitiesManager>().To<AbilityManager>().AsSingle();
            Container.Bind<IAbilitiesUpgradeManager>().To<AbilitiesUpgradeManager>().AsSingle();
            Container.Bind<IPlayerAnimationProvider>().To<PlayerAnimationProvider>().AsSingle();
            Container.Bind<IParticleEffectsProvider>().FromInstance(_particleEffectsProvider);
            Container.Bind<IBossSpawner>().FromInstance(_bossSpawner);
            Container.Bind<IAudioManager>().FromInstance(_audioManager);
        }
    }
}