using AsyncReactAwait.Bindable;
using LaughGame.Model.Audio;
using UnityEngine;

namespace Finespace.LofiLegends.MVVM.Models.Audio
{
    public interface IAudioManager
    {
        AudioConfig AudioConfig { get; }
        IMutable<float> MusicVolume { get; }
        IMutable<float> SfxVolume { get; }
        void Play(AudioClip audioClip);
        void Submit(AudioClip audioClip);
        void SetMusic(AudioClip audioClip, bool loop = true);
        void PauseMusic();
        void StartMusic();
    }
}