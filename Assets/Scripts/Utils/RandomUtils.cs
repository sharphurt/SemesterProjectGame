using UnityEngine;

public static class RandomUtils
{
    public static Vector2 RandomPointInBounds(Bounds bounds) =>
        new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
}