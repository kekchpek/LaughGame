using System;
using System.Collections.Generic;
using AsyncReactAwait.Bindable;
using LaughGame.Model.Audio;
using UnityEngine;

namespace Finespace.LofiLegends.MVVM.Models.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {

        [SerializeField] private AudioSource _sfxAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioConfig _audioConfig;

        private readonly HashSet<AudioClip> _submittedAudioClips = new();

        public AudioConfig AudioConfig => _audioConfig;
        public IMutable<float> MusicVolume { get; } = new Mutable<float>(1f);
        public IMutable<float> SfxVolume { get; } = new Mutable<float>(1f);

        private void Awake()
        {
            MusicVolume.Value = PlayerPrefs.GetFloat("Music", 1f);
            SfxVolume.Value = PlayerPrefs.GetFloat("SFX", 1f);
            SfxVolume.Bind(OnSfxVolumeChanged);
            MusicVolume.Bind(OnMusicVolumeChanged);
        }

        private void OnSfxVolumeChanged(float volume)
        {
            PlayerPrefs.SetFloat("SFX", volume);
            _sfxAudioSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
        
        private void OnMusicVolumeChanged(float volume)
        {
            PlayerPrefs.SetFloat("Music", volume);
            _musicAudioSource.volume = Mathf.Clamp(volume, 0f, 1f);
        }
        
        public void Play(AudioClip audioClip)
        {
            _sfxAudioSource.PlayOneShot(audioClip);
        }

        public void Submit(AudioClip audioClip)
        {
            if (!_submittedAudioClips.Contains(audioClip))
                _submittedAudioClips.Add(audioClip);
        }

        private void Update()
        {
            foreach (var audioClip in _submittedAudioClips)
            {
                Play(audioClip);
            }
            _submittedAudioClips.Clear();
        }

        public void SetMusic(AudioClip audioClip, bool loop = false)
        {
            _musicAudioSource.clip = audioClip;
            _musicAudioSource.loop = loop;
            _musicAudioSource.time = 0f;
            StartMusic();
        }

        public void PauseMusic()
        {
            _musicAudioSource.Pause();
        }

        public void StartMusic()
        {
            _musicAudioSource.Play();
        }
    }
}