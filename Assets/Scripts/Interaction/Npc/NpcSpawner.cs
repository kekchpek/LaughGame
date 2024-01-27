using System;
using UnityEngine;

namespace LaughGame.Interaction.Npc
{
    public class NpcSpawner : MonoBehaviour
    {

        [SerializeField]
        private float _spawnTime;

        private float _time;

        private void Update()
        {
            _time += _spawnTime;
            if (_time >= _spawnTime)
            {
                
            }
        }
    }
}