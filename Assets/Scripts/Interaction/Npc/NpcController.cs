using System.Collections;
using LaughGame.GameResources;
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

        private bool _walking = true;

        private IResourcesService _resourcesService;
        private IPlayerDamageReceiver _playerDamageReceiver;
        private IHappinessManager _happinessManager;

        private Vector3 _velocity;
        private static readonly int Like = Animator.StringToHash("Like");
        private static readonly int Die = Animator.StringToHash("Die");

        [Inject]
        public void Construct(
            IResourcesService resourcesService,
            IPlayerDamageReceiver playerDamageReceiver,
            IHappinessManager happinessManager)
        {
            _resourcesService = resourcesService;
            _playerDamageReceiver = playerDamageReceiver;
            _happinessManager = happinessManager;
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
                    StartCoroutine(Disappear(false));
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
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
        
        private IEnumerator BecomeHappy()
        {
            if (!string.IsNullOrEmpty(_resId))
                yield break;
            _walking = false;
            _collider.enabled = false;
            _animator.SetTrigger(Like);
            _happinessManager.AddHappiness();
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }

        public void TakeDamage(float amount)
        {
            StartCoroutine(BecomeHappy());
        }

    }
}
