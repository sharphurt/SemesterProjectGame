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

        public float movingToDestinationSpeed;

        public delegate void WavesAreOverHandler();

        public event WavesAreOverHandler OnWavesOver;

        private readonly List<Enemy> currentWave = new List<Enemy>();

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

                var instance = SpawnEnemy(GameManager.EnemyPrefabs[e.prefab], e.spawnPosition);
                instance.SetMaxHealth(e.hp, true);
                instance.damage = e.damage;
                instance.MoveTo(e.position, e.movingSpeed != 0f ? e.movingSpeed : movingToDestinationSpeed,
                    e.moveByArc);
            }
        }

        private Enemy SpawnEnemy(Enemy enemy, Vector2 spawnPoint)
        {
            var point = spawnPoint;
            if (spawnPoint == Vector2.zero)
                point = RandomUtils.RandomPointInBounds(spawningArea);

            var instance = Instantiate(enemy, point, Quaternion.identity);
            instance.entityName = enemy.name;
            instance.OnObjectDestroy += id => currentWave.RemoveAll(e => e.GetInstanceID() == id);
            currentWave.Add(instance);
            return instance;
        }
    }
}