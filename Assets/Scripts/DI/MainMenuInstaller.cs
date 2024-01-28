using System;
using Finespace.LofiLegends.MVVM.Models.Audio;
using UnityEngine;
using Zenject;

namespace LaughGame.DI
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private AudioManager _audioManager;
        
        public override void InstallBindings()
        {
            Container.Bind<IAudioManager>().FromInstance(_audioManager);
        }
    }
}