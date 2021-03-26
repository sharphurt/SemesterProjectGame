using Abilities;
using Controllers;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        public float health;
        public float maxHealth;
        public ProgressBarController progressBarController;

        private GettingDamageAbility gettingDamageAbility;

        public delegate void ObjectDestroyHandler(int gameObject);

        public event ObjectDestroyHandler OnObjectDestroy;

        public virtual void Start()
        {
            gettingDamageAbility = GetComponent<GettingDamageAbility>();
            
            progressBarController.SetHealthBar(health, maxHealth, true);
        }

        public void TakeDamage(int damage)
        {
            gettingDamageAbility.Damage = damage;
            health -= gettingDamageAbility.Damage;
            progressBarController.SetHealthBar(health, maxHealth, false);
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
            OnObjectDestroy?.Invoke(GetInstanceID());
        }

        public void SetMaxHealth(float value, bool resetCurrentHealth = false)
        {
            maxHealth = value;
            if (resetCurrentHealth)
                health = maxHealth;
        }
    }
}