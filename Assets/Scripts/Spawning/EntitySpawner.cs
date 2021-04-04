using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Entities;
using LevelData;
using UnityEngine;

namespace Spawning
{
    public class EntitySpawner : MonoBehaviour
    {
        public Bounds spawningArea;

        [HideInInspector] public Bounds destinationArea;
        [HideInInspector] public uint livingEnemiesLimit;
        [HideInInspector] public uint spawningAttempts;

        public float movingToDestinationSpeed;

        public delegate void WavesAreOverHandler();

        public event WavesAreOverHandler OnWavesOver;

        private readonly List<Enemy> currentWave = new List<Enemy>();

        public GameObject booster;
        
        public void SpawnWaves() => StartCoroutine(SpawnWavesCoroutine(GameManager.LevelData.waves));

        private IEnumerator SpawnWavesCoroutine(IEnumerable<WaveData> waves)
        {
            foreach (var wave in waves)
            {
                for (var waveRepeat = 0; waveRepeat < wave.repeats; waveRepeat++)
                {
                    yield return SpawnWaveCoroutine(wave);
                    yield return new WaitWhile(() => currentWave.Any());
                }
            }

            OnWavesOver?.Invoke();
        }

        private IEnumerator SpawnWaveCoroutine(WaveData waveData)
        {
            foreach (var e in waveData.waveElements)
            {
                yield return new WaitForSeconds(e.spawningDelay);

                var instance = SpawnEnemy(GameManager.EnemyPrefabs[e.prefab]);
                instance.SetMaxHealth(e.hp, true);
                instance.damage = e.damage;
                if (e.locationMethod == LocationMethod.Specified)
                    instance.MoveTo(e.position, movingToDestinationSpeed);
                else
                {
                    var pos = RandomUtils.RandomPointInBounds(destinationArea);
                    instance.MoveTo(pos, movingToDestinationSpeed);
                }
            }
        }

        private Enemy SpawnEnemy(Enemy enemy)
        {
            var point = RandomUtils.RandomPointInBounds(spawningArea);
            var instance = Instantiate(enemy, point, Quaternion.identity);
            instance.entityName = enemy.name;
            instance.OnObjectDestroy += id => currentWave.RemoveAll(e => e.GetInstanceID() == id);
            currentWave.Add(instance);
            return instance;
        }
    }
}