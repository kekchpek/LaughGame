using System.Collections;
using System.Collections.Generic;
using Finespace.LofiLegends.MVVM.Models.Audio;
using LaughGame.GameResources;
using LaughGame.Interaction.ParticleEffects;
using LaughGame.Interaction.Win;
using LaughGame.Model.HapinessManager;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace LaughGame.Interaction.Npc
{
    public class NpcController : MonoBehaviour
    {

        [SerializeField]
        private float _speed;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _damage;

        [SerializeField]
        private string _resId;

        [SerializeField]
        private Animator _animator;
        
        [SerializeField]
        private Collider2D _collider;

        [SerializeField]
        private bool _isBoss;

        [SerializeField]
        private float _health;

        private bool _walking = true;

        private Transform _transform;

        private IResourcesService _resourcesService;
        private IPlayerDamageReceiver _playerDamageReceiver;
        private IHappinessManager _happinessManager;
        private IParticleEffectsProvider _particleEffectsProvider;
        private IAudioManager _audioManager;

        private Vector3 _velocity;
        private static readonly int Like = Animator.StringToHash("Like");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Damage = Animator.StringToHash("Damage");

        [Inject]
        public void Construct(
            IResourcesService resourcesService,
            IPlayerDamageReceiver playerDamageReceiver,
            IHappinessManager happinessManager,
            IParticleEffectsProvider particleEffectsProvider,
            IAudioManager audioManager)
        {
            _transform = transform;
            _resourcesService = resourcesService;
            _playerDamageReceiver = playerDamageReceiver;
            _happinessManager = happinessManager;
            _particleEffectsProvider = particleEffectsProvider;
            _audioManager = audioManager;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = default;
            if (_velocity == Vector3.zero)
            {
                SetVelocity(Random.insideUnitCircle.normalized * _speed);
            }

            if (_walking)
            {
                transform.position += _velocity * Time.fixedDeltaTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<NpcPlayerTag>(out _))
            {
                if (!string.IsNullOrEmpty(_resId))
                {
                    _resourcesService.Add(_resId, 1f);
                    StartCoroutine(Disappear(true));
                }
                if (_damage > 0f)
                {
                    _playerDamageReceiver.DoDamage(_damage);
                    if (!_isBoss)
                    {
                        StartCoroutine(Disappear(false));
                    }
                }
            }
            else
            {
                SetVelocity(Quaternion.AngleAxis(Random.Range(-80f, 80f), Vector3.forward) * col.contacts[0].normal.normalized * _speed);
            }
        }

        private void SetVelocity(Vector2 velocity)
        {
            var t = transform;
            _velocity = velocity;
            if (_velocity.x > 0f && t.localScale.x > 0 ||
                _velocity.x < 0f && t.localScale.x < 0)
            {
                var s = t.localScale;
                s.x = -s.x;
                t.localScale = s;
            }
        }

        private IEnumerator Disappear(bool isPositiveAnimation)
        {
            _animator.SetTrigger(Die);
            _collider.enabled = false;
            _walking = false;
            List<AudioClip> clips;
            if (isPositiveAnimation)
            {
                clips = _audioManager.AudioConfig.AllyCollision;
                _particleEffectsProvider.PlayStars(_transform.position);
            }
            else
            {
                clips = _audioManager.AudioConfig.EnemyCollision;
                _particleEffectsProvider.PlayDrops(_transform.position);
            }

            var rand = Random.Range(0, clips.Count);
            _audioManager.Play(clips[rand]);
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
        }
        
        private IEnumerator BecomeHappy()
        {
            if (!string.IsNullOrEmpty(_resId))
                yield break;
            _walking = false;
            _collider.enabled = false;
            _animator.SetTrigger(Like);
            _audioManager.Submit(_audioManager.AudioConfig.EnemyDie);
            _happinessManager.AddHappiness();
            yield return new WaitForSeconds(2.5f);
            Destroy(gameObject);
        }

        public void TakeDamage(float amount)
        {
            if (_isBoss)
            {
                _walking = false;
                _animator.SetTrigger(Damage);
                _health -= amount;
                if (_health <= 0f)
                {
                    _audioManager.Play(_audioManager.AudioConfig.Win);
                    StartCoroutine(Win());
                }
                else
                {
                    _audioManager.Play(_audioManager.AudioConfig.BossHit);
                    StartCoroutine(StartWalking());
                }
            }
            else
            {
                StartCoroutine(BecomeHappy());
            }
        }

        private IEnumerator Win()
        {
            _animator.SetTrigger("Win");
            yield return new WaitForSeconds(2f);
            WinView.Instance.Show();
        }

        private IEnumerator StartWalking()
        {
            yield return new WaitForSeconds(1f);
            _walking = true;
        }
    }
}
