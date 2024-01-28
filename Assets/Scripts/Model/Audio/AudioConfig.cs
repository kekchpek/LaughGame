using System.Collections.Generic;
using UnityEngine;

namespace LaughGame.Model.Audio
{
    
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip MenuMusic;
        public AudioClip BossMusic;
        public AudioClip BattleMusic;
        
        public AudioClip Banana;
        public AudioClip Gun;
        public AudioClip Mic;
        public AudioClip Phone;

        public List<AudioClip> AllyCollision = new();
        public List<AudioClip> EnemyCollision = new();

        public AudioClip Win;
        public AudioClip Lose;

        public AudioClip Button;
        public AudioClip EnemyDie;
        public AudioClip BossHit;
        public AudioClip LevelUp;
        
        public List<AudioClip> Intro = new();

    }
    
}