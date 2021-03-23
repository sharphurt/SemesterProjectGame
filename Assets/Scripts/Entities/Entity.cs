using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        public int health;
        public int maxHealth;
        public HealthBarController healthBarController;

        public delegate void ObjectDestroyHandler(int gameObject);

        public event ObjectDestroyHandler OnObjectDestroy;

        public virtual void Start()
        {
            healthBarController.SetHealthBar(health, maxHealth, true);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            healthBarController.SetHealthBar(health, maxHealth, false);
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
            OnObjectDestroy?.Invoke(GetInstanceID());
        }
    }
}