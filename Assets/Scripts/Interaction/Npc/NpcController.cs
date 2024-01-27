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

        private string _resId;

        private IResourcesService _resourcesService;
        private IDamageReceiver _damageReceiver;

        private Vector3 _velocity;

        [Inject]
        public void Construct(
            IResourcesService resourcesService,
            IDamageReceiver damageReceiver)
        {
            _resourcesService = resourcesService;
            _damageReceiver = damageReceiver;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = default;
            if (_velocity == Vector3.zero)
            {
                _velocity = Random.insideUnitCircle * _speed;
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
                    _damageReceiver.DoDamage(_damage);
                    Disappear(false);
                }
            }
            else
            {
                _velocity = Quaternion.AngleAxis(Random.Range(-90f, 90f), Vector3.forward) * col.contacts[0].normal * _speed;
            }
        }

        private void Disappear(bool isPositiveAnimation)
        {
            Destroy(gameObject);
        }
    }
}
