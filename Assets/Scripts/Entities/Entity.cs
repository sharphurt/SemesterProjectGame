using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Abilities;
using Controllers;
using Items;
using LevelData.LootTable;
using UnityEngine;
using Utils;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        public float health;
        public float maxHealth;

        public string entityName;

        public bool dropsAfterDeath;
        public float dropChance;

        protected Vector3 TargetPosition;
        protected float MoveSpeed;

        protected bool MovingToPoint;
        
        public GameObject deathEffect;

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
            if (item != null)
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
            var normalizedItems = NormalizeItemsChances(lootTable);
            var possibleItems = SelectPossibleItem(normalizedItems, Random.value);

            var booster = possibleItems[Random.Range(0, possibleItems.Count)];

            return GameManager.ItemPrefabs[booster.name];
        }

        private List<BoosterData> NormalizeItemsChances(LootTable lootTable)
        {
            var normalizedChances = lootTable.boosters.Normalize(b => b.chance).ToList();
            var normalizedBoosters = new List<BoosterData>();

            for (var i = 0; i < normalizedChances.Count; i++)
            {
                var boosterData = lootTable.boosters[i];
                var chance = normalizedChances[i] / normalizedChances.Sum();
                normalizedBoosters.Add(new BoosterData {name = boosterData.name, chance = chance});
            }

            return normalizedBoosters;
        }

        private void OnBecameInvisible() => Destroy(gameObject);

        private List<BoosterData> SelectPossibleItem(List<BoosterData> boosters, float chance)
        {
            var possibleBoosters = new List<BoosterData>();

            boosters = boosters.OrderBy(b => b.chance).ToList();

            for (var i = 0; i < boosters.Count; i++)
            {
                var chanceOfBooster = boosters.Take(i).Select(b => b.chance).Sum() + boosters[i].chance;
                if (chanceOfBooster >= chance)
                    possibleBoosters.Add(boosters[i]);
            }

            return possibleBoosters;
        }

        public virtual void Die()
        {
            if (dropsAfterDeath && Random.value <= dropChance)
                DropItem();
            Destroy(gameObject);
            
            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, transform.rotation);

            OnObjectDestroy?.Invoke(GetInstanceID());
        }

        public void SetMaxHealth(float value, bool resetCurrentHealth = false)
        {
            maxHealth = value;
            if (resetCurrentHealth)
                health = maxHealth;
        }

        private void Update()
        {
            UpdatePosition();
        }

        protected virtual void UpdatePosition()
        {
            if (transform.position != new Vector3(TargetPosition.x, TargetPosition.y, 0) && MovingToPoint)
                transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * MoveSpeed);
            else
                MovingToPoint = true;
        }

        public virtual void MoveTo(Vector2 targetPos, float speed)
        {
            MovingToPoint = true;
            TargetPosition = targetPos;
            MoveSpeed = speed;
        }
    }
}