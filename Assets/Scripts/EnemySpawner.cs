using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemy;
    public float spawnPeriod;

    public Bounds spawningArea;
    public Bounds destinationArea;

    public float movingToDestinationSpeed;
    public uint livingEnemiesLimit;

    public uint spawningAttempts;

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
        var point = RandomPointInBounds(spawningArea);
        var created = Instantiate(enemy, point, Quaternion.identity);

        if (!TryFindDestinationPoint(created, out var destPoint))
            Destroy(created.gameObject);
        else
            created.MoveToPosition(destPoint, movingToDestinationSpeed);
    }

    private bool TryFindDestinationPoint(Component e, out Vector2 destPoint)
    {
        if (CountEnemiesOnScene() > livingEnemiesLimit)
        {
            Debug.LogWarning("It is impossible to spawn an entity - enemies limit was reached");
            destPoint = Vector2.zero;
            return false;
        }

        for (var attempt = 0; attempt < spawningAttempts; attempt++)
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
        var destPoint = RandomPointInBounds(destinationArea);
        bounds.center = destPoint;
        DrawBounds(bounds, CheckIntersectsWithLivingEnemies(bounds) ? Color.red : Color.green, 3f);
        return !CheckIntersectsWithLivingEnemies(bounds) ? destPoint : Vector2.zero;
    }

    private static bool CheckIntersectsWithLivingEnemies(Bounds bounds)
    {
        return GameObject.FindGameObjectsWithTag("Enemy")
            .Where(e => e.GetComponent<BoxCollider2D>().bounds != bounds)
            .Any(spawnedEnemy => spawnedEnemy.GetComponent<BoxCollider2D>().bounds.Intersects(bounds));
    }

    private static Vector2 RandomPointInBounds(Bounds bounds) =>
        new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));

    private int CountEnemiesOnScene() => GameObject.FindGameObjectsWithTag("Enemy").Length;

    public static void DrawBounds(Bounds b, Color color, float delay = 0)
    {
        // bottom
        var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
        var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
        var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
        var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

        Debug.DrawLine(p1, p2, color, delay);
        Debug.DrawLine(p2, p3, color, delay);
        Debug.DrawLine(p3, p4, color, delay);
        Debug.DrawLine(p4, p1, color, delay);

        // top
        var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
        var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
        var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
        var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

        Debug.DrawLine(p5, p6, color, delay);
        Debug.DrawLine(p6, p7, color, delay);
        Debug.DrawLine(p7, p8, color, delay);
        Debug.DrawLine(p8, p5, color, delay);

        // sides
        Debug.DrawLine(p1, p5, color, delay);
        Debug.DrawLine(p2, p6, color, delay);
        Debug.DrawLine(p3, p7, color, delay);
        Debug.DrawLine(p4, p8, color, delay);
    }
}