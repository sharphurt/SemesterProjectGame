using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Controllers;
using Modifiers;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        public int health;
        public int maxHealth;
        public ProgressBarController progressBarController;
        
        public delegate void ObjectDestroyHandler(int gameObject);

        public event ObjectDestroyHandler OnObjectDestroy;

        public virtual void Start()
        {
            progressBarController.SetHealthBar(health, maxHealth, true);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            progressBarController.SetHealthBar(health, maxHealth, false);
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