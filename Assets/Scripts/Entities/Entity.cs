using System.Collections.Generic;
using System.Linq;
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

        [HideInInspector] public string entityName;

        public bool dropsAfterDeath;
        public float boosterDropChance;
        public float partsDropChance;

        public float damage;

        private Vector3 targetPosition;
        private float moveSpeed;
        private bool movingToPoint;

        private bool moveByArc;
        private float count;
        private Vector2 middlePoint;
        private Vector2 startPosition;

        public GameObject deathEffect;

        private CoinsCollector coinsCollector;

        public ProgressBarController progressBarController;

        private GettingDamageAbility gettingDamageAbility;

        public delegate void ObjectDestroyHandler(int gameObject);

        public event ObjectDestroyHandler OnObjectDestroy;

        private AudioSource damageSource;
        private AudioSource carCrashSource;

        public virtual void Start()
        {
            gettingDamageAbility = GetComponent<GettingDamageAbility>();
            progressBarController.SetHealthBar(health, maxHealth);
            coinsCollector = FindObjectOfType<CoinsCollector>();
            damageSource = GameObject.Find("DamageSound").GetComponent<AudioSource>();
            carCrashSource = GameObject.Find("CarCrashSound").GetComponent<AudioSource>();
        }

        public virtual void TakeDamage(float damage)
        {
            damageSource.volume = CompareTag("Player") ? 0.6f : 0.3f;
            damageSource.Play();
            gettingDamageAbility.Damage = damage;
            health -= gettingDamageAbility.Damage;
            progressBarController.SetHealthBar(health, maxHealth);
            if (health <= 0)
                Die();
        }

        public virtual void Heal(float value)
        {
            health = Mathf.Clamp(health + value, 0, maxHealth);
            progressBarController.SetHealthBar(health, maxHealth);
        }

        public virtual void Die()
        {
            carCrashSource.Play();
            
            if (dropsAfterDeath && Random.value <= boosterDropChance)
                DropBooster();

            if (dropsAfterDeath && Random.value <= partsDropChance)
                DropParts();

            Destroy(gameObject);

            if (deathEffect != null)
                Instantiate(deathEffect, transform.position, transform.rotation);

            OnObjectDestroy?.Invoke(GetInstanceID());
        }

        private void DropBooster()
        {
            var item = SelectBoosterToDrop();
            if (item != null)
                Instantiate(item, transform.position, Quaternion.identity);
        }

        private void DropParts()
        {
            var partsCount = Random.Range(1, GameManager.LootTables[entityName.Replace("(Clone)", "")].partsMaxCount[gameObject.scene.name]);
            for (var i = 0; i < partsCount; i++)
            {
                coinsCollector.StartCoinMove(transform.position,
                    i * 0.1f / ((Mathf.Log10(partsCount) + 1) * partsCount / 5));
            }
        }

        private Item SelectBoosterToDrop()
        {
            if (!GameManager.LootTables.ContainsKey(entityName.Replace("(Clone)", "")))
            {
                Debug.LogError($"There is no loot table for entity \"{name}\"");
                return null;
            }

            var lootTable = GameManager.LootTables[entityName.Replace("(Clone)", "")];
            var normalizedItems = NormalizeBoosterChances(lootTable);
            var possibleBooster = SelectPossibleBooster(normalizedItems, Random.value);

            var booster = possibleBooster[Random.Range(0, possibleBooster.Count)];

            return GameManager.ItemPrefabs[booster.name];
        }

        private List<BoosterData> NormalizeBoosterChances(LootTable lootTable)
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

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
            OnObjectDestroy?.Invoke(GetInstanceID());
        }

        private List<BoosterData> SelectPossibleBooster(List<BoosterData> boosters, float chance)
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


        public void SetMaxHealth(float value, bool resetCurrentHealth = false)
        {
            maxHealth = value;
            if (resetCurrentHealth)
                health = maxHealth;
        }

        private void Update() => UpdatePosition();

        private void UpdatePosition()
        {
            if (moveByArc)
            {
                if (count < 1.0f && movingToPoint)
                {
                    count += moveSpeed * Time.deltaTime;

                    Vector3 m1 = Vector2.Lerp(startPosition, middlePoint, count);
                    Vector3 m2 = Vector2.Lerp(middlePoint, targetPosition, count);
                    transform.position = Vector2.Lerp(m1, m2, count);
                    var rotation = Vector2Utils.CalculateFacingToTarget(m1, m2).angle;
                    transform.rotation = rotation;
                }
            }
            else
            {
                if (transform.position != new Vector3(targetPosition.x, targetPosition.y, 0) && movingToPoint)
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
                else
                    movingToPoint = true;
            }
        }

        public void MoveTo(Vector2 targetPos, float speed, bool byArc)
        {
            movingToPoint = true;
            targetPosition = targetPos;
            moveSpeed = speed;
            moveByArc = byArc;

            if (byArc)
            {
                startPosition = transform.position;
                middlePoint = startPosition + (targetPos - startPosition) / 2 +
                              Vector2.left * ((startPosition.x - targetPos.x) * 0.6f);
            }
        }
    }
}