using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnPeriod;

    public Vector2 leftEdge;
    public Vector2 rightEdge;

    void Start() => StartCoroutine(nameof(DoTaskPeriodically));

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
        var point = RandomPoint(leftEdge, rightEdge);
        Instantiate(enemy, point, Quaternion.identity);
    }

    private static Vector2 RandomPoint(Vector2 min, Vector2 max) =>
        new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
}