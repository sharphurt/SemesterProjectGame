using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Bounds spawningArea;
    public Bounds destinationArea;

    public float movingToDestinationSpeed;
    public uint livingEnemiesLimit;

    public uint spawningAttempts;

    private LevelData levelData;
    private Dictionary<string, Enemy> prefabs;

    private List<Enemy> currentWave = new List<Enemy>();

    void Start()
    {
        levelData = FindObjectOfType<LevelDataLoader>().levelData;
        prefabs = PreparePrefabs();

        StartCoroutine(SpawnWave(levelData.wavesData[0]));
    }

    private Dictionary<string, Enemy> PreparePrefabs() =>
        levelData.wavesData
            .SelectMany(s => s.waveElements)
            .GroupBy(e => e.enemy)
            .Select(g => g.First().enemy)
            .ToDictionary(k => k, v => Resources.Load<Enemy>($"Prefabs/Enemies/{v}"));

    private Enemy Spawn(Enemy enemy)
    {
        var point = RandomUtils.RandomPointInBounds(spawningArea);
        return Instantiate(enemy, point, Quaternion.identity);
    }

    private IEnumerator SpawnWave(WaveData waveData)
    {
        foreach (var e in waveData.waveElements)
        {
            var instance = Spawn(prefabs[e.enemy]);
            instance.OnObjectDestroy += RemoveDestroyedObjectFromWave;
            currentWave.Add(instance);
            if (e.locationMethod == LocationMethod.Specified)
                instance.MoveToPosition(e.position, movingToDestinationSpeed);
            else
            {
                var pos = RandomUtils.RandomPointInBounds(destinationArea);
                instance.MoveToPosition(pos, movingToDestinationSpeed);
            }

            yield return new WaitForSeconds(waveData.spawningDelay);
        }
    }

    private void RemoveDestroyedObjectFromWave(int id) => currentWave.RemoveAll(e => e.GetInstanceID() == id);

  
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