using System.Linq;
using AsyncReactAwait.Bindable;
using LaughGame.GameResources;
using LaughGame.Interaction.PlayerAnimations;
using LaughGame.Model.AbilitiesManagement;
using LaughGame.Model.AbilitiesUpgrade;
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
        private Color _notAvailableColor;
        
        [SerializeField]
        private Color _availableColor;
        
        [SerializeField]
        private Image _bg;

        [SerializeField]
        private PriceView _priceView;

        [SerializeField]
        private StarsView _stars;

        private IResourcesModel _resourcesModel;

        private IAbilitiesManager _abilitiesManager;
        private IPlayerAnimationProvider _playerAnimationProvider;
        private IBindable<int> _level;

        [Inject]
        public void Construct(
            IAbilitiesManager abilitiesManager,
            IResourcesModel resourcesModel,
            IPlayerAnimationProvider playerAnimationProvider)
        {
            _resourcesModel = resourcesModel;
            _abilitiesManager = abilitiesManager;
            _playerAnimationProvider = playerAnimationProvider;
            _abilitiesManager.AbilityUpdated += OnAbilityChanged;
            _resourcesModel.ResourceChanged += OnResourceChanged;
            UpdateAbility();
        }

        private void OnResourceChanged(ResourceId arg1, float arg2)
        {
            UpdateIsAvailable();
        }

        private void OnAbilityChanged(int index)
        {
            if (index == _index)
            {
                UpdateAbility();
            }
        }

        private void UpdateIsAvailable()
        {
            var isAvailable = _abilitiesManager.Get(_index)!.Value.Price
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .GroupBy(x => x)
                .Select(x => (x.Key, (float)x.Count()))
                .Aggregate(true, (s, x ) => s && _resourcesModel.GetResource(x.Key).Value >= x.Item2);
            _bg.color = isAvailable ? _availableColor : _notAvailableColor;

        }

        private void UpdateAbility()
        {
            var data = _abilitiesManager.Get(_index);
            if (!data.HasValue || data.Value.Ability == null)
                return;

            _image.sprite = data.Value.Ability.GetSprite();
            _priceView.SetPrice(data.Value.Price);
            if (_level != null)
            {
                _level.Unbind(_stars.SetStars);
            }
            _level = data.Value.Ability.CurrentLevel;
            _level.Bind(_stars.SetStars);
            UpdateIsAvailable();
        }

        public void Update()
        {
            if (Time.timeScale > 0f && Input.GetKeyDown((_index + 1).ToString()))
            {
                var anim = GetAbilityAnimation();
                if (_abilitiesManager.TryUse(_index))
                {
                    _playerAnimationProvider.PlaySkill(anim);
                }
            }
        }

        private string GetAbilityAnimation()
        {
            return _abilitiesManager.Get(_index)!.Value.Ability.AnimationName;
        }

        private void OnDestroy()
        {
            _abilitiesManager.AbilityUpdated -= OnAbilityChanged;
            _resourcesModel.ResourceChanged -= OnResourceChanged;
            if (_level != null)
            {
                _level.Unbind(_stars.SetStars);
            }
        }
    }
}