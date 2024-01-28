using Finespace.LofiLegends.MVVM.Models.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace LaughGame.Interaction.MenuBar
{
    public class MenuBarView : MonoBehaviour
    {

        private IAudioManager _audioManager;

        [SerializeField]
        private Button _soundButton;
        [SerializeField]
        private Button _musicButton;
        [SerializeField]
        private Button _exitButton;

        [SerializeField]
        private Image _soundImage;

        [SerializeField]
        private Image _musicImage;

        [SerializeField]
        private Sprite _soundOn;
        [SerializeField]
        private Sprite _soundOff;
        
        [SerializeField]
        private Sprite _musicOn;
        [SerializeField]
        private Sprite _musicOff;
        
        [Inject]
        public void Construct(IAudioManager audioManager)
        {
            _audioManager = audioManager;
            _audioManager.MusicVolume.Bind(OnMusicChanged);
            _audioManager.SfxVolume.Bind(OnSfxChanged);
            
            _soundButton.onClick.AddListener(OnSoundClicked);
            _musicButton.onClick.AddListener(OnMusicClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnExitClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnMusicClicked()
        {
            if (_audioManager.MusicVolume.Value > 0f)
            {
                _audioManager.MusicVolume.Value = 0f;
            }
            else
            {
                _audioManager.MusicVolume.Value = 1f;
            }
        }

        private void OnSoundClicked()
        {
            if (_audioManager.SfxVolume.Value > 0f)
            {
                _audioManager.SfxVolume.Value = 0f;
            }
            else
            {
                _audioManager.SfxVolume.Value = 1f;
            }
        }

        private void OnMusicChanged(float music)
        {
            if (music > 0f)
            {
                _musicImage.sprite = _musicOn;
            }
            else
            {
                _musicImage.sprite = _musicOff;
            }
        }

        private void OnSfxChanged(float sfx)
        {
            if (sfx > 0f)
            {
                _soundImage.sprite = _soundOn;
            }
            else
            {
                _soundImage.sprite = _soundOff;
            }
        }

        private void OnDestroy()
        {
            _soundButton.onClick.RemoveListener(OnSoundClicked);
            _musicButton.onClick.RemoveListener(OnMusicClicked);
            _exitButton.onClick.RemoveListener(OnExitClicked);
        }
    }
}