using System;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.Boss
{
    public class BossSpawner : MonoBehaviour, IBossSpawner
    {

        [SerializeField]
        private GameObject _bossPrefab;

        private IInstantiator _instantiator;

        private bool _bossSpawned;

        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public void SpawnBoss()
        {
            if (_bossSpawned)
                return;
            var boss = _instantiator.InstantiatePrefab(_bossPrefab);
            boss.transform.position = transform.position;
            _bossSpawned = true;
        }
    }
}