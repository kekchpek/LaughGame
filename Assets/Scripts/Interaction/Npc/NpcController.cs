using LaughGame.GameResources;
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

        private IResourcesService _resourcesService;
        private IPlayerDamageReceiver _playerDamageReceiver;

        private Vector3 _velocity;

        [Inject]
        public void Construct(
            IResourcesService resourcesService,
            IPlayerDamageReceiver playerDamageReceiver)
        {
            _resourcesService = resourcesService;
            _playerDamageReceiver = playerDamageReceiver;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = default;
            if (_velocity == Vector3.zero)
            {
                SetVelocity(Random.insideUnitCircle.normalized * _speed);
            }

            transform.position += _velocity * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent<NpcPlayerTag>(out _))
            {
                if (!string.IsNullOrEmpty(_resId))
                {
                    _resourcesService.Add(_resId, 1f);
                    Disappear(true);
                }
                if (_damage > 0f)
                {
                    _playerDamageReceiver.DoDamage(_damage);
                    Disappear(false);
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

        private void Disappear(bool isPositiveAnimation)
        {
            Destroy(gameObject);
        }
    }
}
