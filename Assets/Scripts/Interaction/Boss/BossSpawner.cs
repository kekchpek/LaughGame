using System;
using Finespace.LofiLegends.MVVM.Models.Audio;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.Boss
{
    public class BossSpawner : MonoBehaviour, IBossSpawner
    {

        [SerializeField]
        private GameObject _bossPrefab;

        private IInstantiator _instantiator;
        private IAudioManager _audioManager;

        private bool _bossSpawned;

        [Inject]
        public void Construct(IInstantiator instantiator,
            IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _instantiator = instantiator;
        }
        
        public void SpawnBoss()
        {
            if (_bossSpawned)
                return;
            _audioManager.SetMusic(_audioManager.AudioConfig.BossMusic);
            var boss = _instantiator.InstantiatePrefab(_bossPrefab);
            boss.transform.position = transform.position;
            _bossSpawned = true;
        }
    }
}