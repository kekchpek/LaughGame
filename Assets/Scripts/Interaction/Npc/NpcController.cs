using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LaughGame.Interaction.Npc
{
    public class NpcController : MonoBehaviour
    {

        [SerializeField]
        private float _speed;

        private Vector3 _velocity;

        private void FixedUpdate()
        {
            if (_velocity == Vector3.zero)
            {
                _velocity = Random.insideUnitCircle * _speed;
            }

            transform.position += _velocity * Time.fixedDeltaTime;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _velocity = Quaternion.AngleAxis(Random.Range(-90f, 90f), Vector3.forward) * col.contacts[0].normal * _speed;
        }
    }
}
