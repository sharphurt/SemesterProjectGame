using System.Linq;
using Abilities;
using Controllers;
using Items;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        public float health;
        public float maxHealth;

        public string entityName;

        public bool dropsAfterDeath;
        public float dropChance;

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

        private void DropItem()
        {
            var item = SelectItemToDrop();
            Instantiate(item, transform.position, Quaternion.identity);
        }

        private Item SelectItemToDrop()
        {
            if (!GameManager.LootTables.ContainsKey(entityName))
            {
                Debug.LogError($"There is no loot table for entity \"{name}\"");
                return null;
            }

            var lootTable = GameManager.LootTables[entityName];
            var possibleBoosters = lootTable.boosters.Where(b => Random.value <= b.chance).ToList();
            var booster = possibleBoosters[Random.Range(0, possibleBoosters.Count)];
            return GameManager.ItemPrefabs[booster.name];
        }

        public virtual void Die()
        {
            if (dropsAfterDeath && Random.value <= dropChance)
                DropItem();
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