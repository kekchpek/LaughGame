using LaughGame.Interaction.Lose;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LaughGame.Interaction.Win
{
    public class WinView : MonoBehaviour
    {
        
        public static WinView Instance { get; private set; }

        [FormerlySerializedAs("_tryAgain")]
        [SerializeField]
        private Button _exit;

        private void Awake()
        {
            Instance = this;
            _exit.onClick.AddListener(OnTryAgain);
            gameObject.SetActive(false);
        }

        public void Show()
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }

        private void OnTryAgain()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}