using LaughGame.Assets.Scripts.Model.Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LaughGame.Assets.Scripts.Model.Abilities.Test
{
    

    public class TestHealth : MonoBehaviour, IHealth
    {
        [SerializeField] public float MaxHealth;
        private float _curHealth;


        private void Start()
        {
            _curHealth = MaxHealth;
        }
        public void Die()
        {
            
            Destroy(gameObject);
        }



        public void TakeDamage(float amount)
        {
            _curHealth = Mathf.Clamp(_curHealth - amount, 0, MaxHealth);

            if (_curHealth <= 0)
                Die();
        }

    }
}
