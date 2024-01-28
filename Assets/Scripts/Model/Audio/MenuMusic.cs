using Finespace.LofiLegends.MVVM.Models.Audio;
using UnityEngine;
using Zenject;

namespace LaughGame.Model.Audio
{
    public class MenuMusic : MonoBehaviour
    {

        [Inject]
        public void Construct(IAudioManager audioManager)
        {
            audioManager.SetMusic(audioManager.AudioConfig.MenuMusic);
        }
        
    }
}