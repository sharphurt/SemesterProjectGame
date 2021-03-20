using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemy;
    public float spawnPeriod;

    public Bounds spawningArea;
    public Bounds destinationArea;

    public float movingToDestinationSpeed;

    void Start()
    {
        /*Debug.Log(spawningArea.bounds);
        Debug.Log(destinationArea.bounds);*/
        StartCoroutine(nameof(DoTaskPeriodically));
    }

    public IEnumerator DoTaskPeriodically()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnPeriod);
        }
    }

    private void Spawn()
    {
        var point = RandomPointInBounds(spawningArea);
        var created = Instantiate(enemy, point, Quaternion.identity);
        var destPoint = RandomPointInBounds(destinationArea);
        created.MoveToPosition(destPoint, movingToDestinationSpeed);
    }

    private static Vector2 RandomPointInBounds(Bounds bounds) =>
        new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
}