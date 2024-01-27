using System.Collections.Generic;
using System.Linq;
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
        private List<Transform> _spawnPoints;

        private float _time;

        private IInstantiator _instantiator;
        private IPlayerPositionProvider _playerPositionProvider;

        [Inject]
        public void Construct(IInstantiator instantiator,
            IPlayerPositionProvider playerPositionProvider)
        {
            _instantiator = instantiator;
            _playerPositionProvider = playerPositionProvider;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _spawnTime)
            {
                Spawn();
                _spawnTime *= 0.99f;
                _time = 0f;
            }
        }

        private void Spawn()
        {
            var chance = Random.value;
            GameObject npcToSpawn;
            if (chance < _enemyChance)
            {
                var randIndex = Random.Range(0, _enemyPrefabs.Count);
                npcToSpawn = _enemyPrefabs[randIndex];
            }
            else
            {
                var randIndex = Random.Range(0, _alliesPrefabs.Count);
                npcToSpawn = _alliesPrefabs[randIndex];
            }
            var npc = _instantiator.InstantiatePrefab(npcToSpawn);
            var playerPos = _playerPositionProvider.GetPosition();
            if (!playerPos.HasValue)
                return;
            var availableSpawns = _spawnPoints
                .Where(x => Vector3.Distance(x.position, playerPos.Value) > 10f)
                .ToArray();
            var spawnIndex = Random.Range(0, availableSpawns.Length);
            var position = availableSpawns[spawnIndex].position;
            npc.transform.position = position;
        }
    }
}