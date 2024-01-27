using LaughGame.Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LaughGame.Assets.Scripts.Model.Abilities
{
    class CircleAbility : MonoBehaviour, IAbility
    {
        [field: SerializeField] public IMovable AbilityParent { get; private set; }
        [SerializeField] float _damage;
        [SerializeField] float _radius;

        [Inject]
        public void Construct(IAbilitiesEntitiesProvider entitiesProvider)
        {
            AbilityParent = entitiesProvider.GetMovablePlayer();
        }

        public void Execute()
        {

            var colliders = Physics.OverlapSphere(
                AbilityParent.MovableTransform.position,
                _radius,
                AbilitiesConfig.EnemiesLayerMask);

            foreach (var collider in colliders)
            {
                var health = collider.GetComponent<IHealth>();
                if (health != null)
                    health.TakeDamage(_damage);
            }
        }


    }
}
