using LaughGame.Model.HapinessManager;
using Shared;
using UnityEngine;
using Zenject;

namespace LaughGame.Interaction.UI
{
    
    [RequireComponent(typeof(ProgressBar))]
    public class HappinessProgress : MonoBehaviour
    {

        private ProgressBar _progressBar;
        private IHappinessManager _happinessManager;

        [Inject]
        public void Construct(IHappinessManager happinessManager)
        {
            _progressBar = GetComponent<ProgressBar>();
            _happinessManager = happinessManager;
            _progressBar.MaxValue = _happinessManager.MaxHappiness;
            _happinessManager.Happiness.Bind(OnHappinessChanged);
        }

        private void OnHappinessChanged(float val)
        {
            _progressBar.Value = val;
        }

        private void OnDestroy()
        {
            _happinessManager.Happiness.Unbind(OnHappinessChanged);
        }
    }
}