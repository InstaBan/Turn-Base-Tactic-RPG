using System;
using UnityEngine;

namespace LuminaStudio.Entity
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] 
        private int _health = 100;

        public event EventHandler OnDeath;

        public void ReceiveDamage(int amount)
        {
            _health -= amount;
            if (_health <= 0)
            {
                _health = 0;
            }

            if (_health == 0)
            {
                Die();
            }
            Debug.Log("Current Health: " + _health);
        }

        private void Die()
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
