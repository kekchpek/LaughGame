using Finespace.LofiLegends.MVVM.Models.Audio;
using UnityEngine;
using Zenject;

namespace LaughGame.Model.Audio
{
    public class LevelMusic : MonoBehaviour
    {

        [Inject]
        public void Construct(IAudioManager audioManager)
        {
            audioManager.SetMusic(audioManager.AudioConfig.BattleMusic);
        }
        
    }
}