using System;
using AsyncReactAwait.Bindable;
using Finespace.LofiLegends.MVVM.Models.Audio;
using LaughGame.Interaction.Boss;
using LaughGame.Interaction.Lose;
using LaughGame.Interaction.PlayerAnimations;
using LaughGame.Model.AbilitiesUpgrade;

namespace LaughGame.Model.HapinessManager
{
    public class HappinessManager : IHappinessManager, IDisposable
    {
        private readonly IAbilitiesUpgradeManager _upgradeManager;
        private readonly IPlayerAnimationProvider _playerAnimationProvider;
        private readonly IBossSpawner _bossSpawner;
        private readonly IAudioManager _audioManager;
        private const float MaxHappinessValue = 100f;
        private const float HappinessBonus = 2f;

        private readonly IMutable<float> _happiness = new Mutable<float>(50f);

        private int _level;

        public float MaxHappiness => MaxHappinessValue;
        public IBindable<float> Happiness => _happiness;

        public HappinessManager(
            IAbilitiesUpgradeManager upgradeManager,
            IPlayerAnimationProvider playerAnimationProvider,
            IBossSpawner bossSpawner,
            IAudioManager audioManager)
        {
            _upgradeManager = upgradeManager;
            _playerAnimationProvider = playerAnimationProvider;
            _bossSpawner = bossSpawner;
            _audioManager = audioManager;
            _happiness.Bind(OnHappinessChanged);
        }

        private void OnHappinessChanged(float val)
        {
            _playerAnimationProvider.SetFaceValue(val);
        }

        public void SetHappinessPercent(float happinessPercent)
        {
            _happiness.Value = MaxHappiness * happinessPercent;
        }

        public async void AddHappiness()
        {
            _happiness.Value += HappinessBonus;
            while (_happiness.Value > MaxHappinessValue)
            {
                if (_upgradeManager.CanUpgrade())
                {
                    _audioManager.Play(_audioManager.AudioConfig.LevelUp);
                }

                var isUpgraded = await _upgradeManager.StartUpgrade();
                if (isUpgraded)
                    _happiness.Value -= MaxHappinessValue * 0.5f;
                else
                    _happiness.Value = MaxHappinessValue;
                _level++;
            }

            if (_level > 1)
            {
                _bossSpawner.SpawnBoss();
            }
        }

        public void SubtractHappiness(float val)
        {
            _happiness.Value -= val;
            if (_happiness.Value < 0f)
            {
                _audioManager.Play(_audioManager.AudioConfig.Lose);
                LoseView.Instance.Show();
            }
        }

        public void Dispose()
        {
            _happiness.Unbind(OnHappinessChanged);
        }
    }
}