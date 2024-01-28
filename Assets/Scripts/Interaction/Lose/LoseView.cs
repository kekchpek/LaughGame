using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LaughGame.Interaction.Lose
{
    public class LoseView : MonoBehaviour
    {
        
        public static LoseView Instance { get; private set; }

        [SerializeField]
        private Button _tryAgain;

        private void Awake()
        {
            Instance = this;
            _tryAgain.onClick.AddListener(OnTryAgain);
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
            SceneManager.LoadScene("SampleScene");
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}