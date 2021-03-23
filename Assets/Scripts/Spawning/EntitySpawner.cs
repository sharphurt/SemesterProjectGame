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

                var instance = Spawn(GameManager.Prefabs[e.enemy]);
                if (e.locationMethod == LocationMethod.Specified)
                    instance.MoveTo(e.position, movingToDestinationSpeed);
                else
                {
                    var pos = RandomUtils.RandomPointInBounds(destinationArea);
                    instance.MoveTo(pos, movingToDestinationSpeed);
                }
            }
        }

        private Enemy Spawn(Enemy enemy)
        {
            var point = RandomUtils.RandomPointInBounds(spawningArea);
            var instance = Instantiate(enemy, point, Quaternion.identity);
            instance.OnObjectDestroy += id => currentWave.RemoveAll(e => e.GetInstanceID() == id);
            currentWave.Add(instance);
            return instance;
        }


        #region Finding random location

        private bool TryFindDestinationPoint(Component e, out Vector2 destPoint)
        {
            if (CountEnemiesOnScene() > livingEnemiesLimit)
            {
                Debug.LogWarning("It is impossible to spawn an entity - enemies limit was reached");
                destPoint = Vector2.zero;
                return false;
            }

            for (var attempt = 0;
                attempt < spawningAttempts;
                attempt++)
            {
                var dest = FindFreeDestinationPoint(e.GetComponent<BoxCollider2D>().bounds);
                if (dest != Vector2.zero)
                {
                    destPoint = dest;
                    return true;
                }
            }

            Debug.LogWarning("It is impossible to spawn an entity - no free space was found");
            destPoint = Vector2.zero;
            return false;
        }

        private Vector2 FindFreeDestinationPoint(Bounds bounds)
        {
            var destPoint = RandomUtils.RandomPointInBounds(destinationArea);
            bounds.center = destPoint;
            return !CheckIntersectsWithLivingEnemies(bounds) ? destPoint : Vector2.zero;
        }

        private static bool CheckIntersectsWithLivingEnemies(Bounds bounds)
        {
            return GameObject.FindGameObjectsWithTag("Enemy")
                .Where(e => e.GetComponent<BoxCollider2D>().bounds != bounds)
                .Any(spawnedEnemy => spawnedEnemy.GetComponent<BoxCollider2D>().bounds.Intersects(bounds));
        }

        private int CountEnemiesOnScene() => GameObject.FindGameObjectsWithTag("Enemy").Length;

        #endregion
    }
}