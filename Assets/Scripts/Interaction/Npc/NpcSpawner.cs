using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace LaughGame.Interaction.Npc
{
    public class NpcSpawner : MonoBehaviour
    {

        [SerializeField]
        [Range(0f, 1f)]
        private float _enemyChance;

        [SerializeField]
        private float _spawnTime;

        [SerializeField]
        private List<GameObject> _enemyPrefabs = new();

        [SerializeField]
        private List<GameObject> _alliesPrefabs = new();

        [SerializeField]
        private Rect _spawnRect;

        private float _time;

        private IInstantiator _instantiator;

        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        private void Update()
        {
            _time += _spawnTime;
            if (_time >= _spawnTime)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var chance = Random.value;
            GameObject npcToSpawn;
            if (chance < _enemyChance)
            {
                var randIndex = Random.Range(0, _enemyPrefabs.Count - 1);
                npcToSpawn = _enemyPrefabs[randIndex];
            }
            else
            {
                var randIndex = Random.Range(0, _alliesPrefabs.Count - 1);
                npcToSpawn = _enemyPrefabs[randIndex];
            }
            var npc = _instantiator.InstantiatePrefab(npcToSpawn);
            var position = new Vector3(
                Random.Range(_spawnRect.xMin, _spawnRect.xMax),
                Random.Range(_spawnRect.yMin, _spawnRect.yMax));
            npc.transform.position = position;
        }
    }
}