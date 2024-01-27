using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaughGame.Assets.Scripts.Model.Abilities.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace LaughGame.Model.AbilitiesUpgrade
{
    public class AbilitiesPopup : MonoBehaviour, IAbilitiesPopup
    {

        [Serializable]
        public struct OptionData
        {

            public Button Button => _button;
            public Image Image => _image;
            public TMP_Text Text => _text;
            public GameObject GameObject => _gameObject;
            public StarsView Stars => _stars;

            [SerializeField]
            private Button _button;
            [SerializeField]
            private Image _image;
            [SerializeField]
            private TMP_Text _text;
            [SerializeField]
            private GameObject _gameObject;
            [SerializeField]
            private StarsView _stars;
        }
        
        [SerializeField]
        private List<OptionData> _options = new();

        private IAbilitiesUpgradeManager _upgradeManager;

        [Inject]
        public void Construct(IAbilitiesUpgradeManager upgradeManager)
        {
            _upgradeManager = upgradeManager;
            _upgradeManager.SetPopup(this);
        }

        public Task Upgrade(IAbility[] abilitiesToChose)
        {
            gameObject.SetActive(true);
            var tcs = new TaskCompletionSource<bool>();

            foreach (var o in _options)
            {
                o.GameObject.SetActive(false);
                o.Button.onClick.RemoveAllListeners();
            }

            var i = 0;
            foreach (var ability in abilitiesToChose)
            {
                void OnClick()
                {
                    ability.Upgrade();
                    gameObject.SetActive(false);
                    tcs.SetResult(true);
                }
                _options[i].GameObject.SetActive(true);
                _options[i].Image.sprite = ability.GetSprite();
                _options[i].Stars.SetStars(ability.CurrentLevel + 1);
                _options[i].Text.text = ability.UpgradeDescription;
                _options[i].Button.onClick.AddListener(OnClick);
                i++;
            }
            
            return tcs.Task;
        }


        private void OnDestroy()
        {
            _upgradeManager.SetPopup(null);
        }
    }
}