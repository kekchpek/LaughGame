using System;
using Finespace.LofiLegends.MVVM.Models.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LaughGame.Model.Audio
{
    
    [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour
    {
        private IAudioManager _audioManager;

        private Button _button;
        
        [Inject]
        public void Construct(IAudioManager audioManager)
        {
            _button = GetComponent<Button>();
            _audioManager = audioManager;
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _audioManager.Play(_audioManager.AudioConfig.Button);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}