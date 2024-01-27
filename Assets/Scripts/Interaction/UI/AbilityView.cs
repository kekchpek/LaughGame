using LaughGame.Model.AbilitiesManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LaughGame.Interaction.UI
{
    public class AbilityView : MonoBehaviour
    {

        [SerializeField]
        private int _index;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private PriceView _priceView;

        private IAbilitiesManager _abilitiesManager;

        [Inject]
        public void Construct(IAbilitiesManager abilitiesManager)
        {
            _abilitiesManager = abilitiesManager;
            _abilitiesManager.AbilityUpdated += OnAbilityChanged;
        }

        private void OnAbilityChanged(int index)
        {
            if (index == _index)
            {
                UpdateAbility();
            }
        }

        private void UpdateAbility()
        {
            var data = _abilitiesManager.Get(_index);
            _image.sprite = data.Ability.GetSprite();
            _priceView.SetPrice(data.Price);
        }

        public void Update()
        {
            if (Input.GetKeyDown((_index + 1).ToString()))
            {
                _abilitiesManager.Use(_index);
            }
        }

        private void OnDestroy()
        {
            _abilitiesManager.AbilityUpdated -= OnAbilityChanged;
        }
    }
}