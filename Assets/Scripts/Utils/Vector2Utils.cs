using UnityEngine;

namespace Utils
{
    public static class Vector2Utils
    {
        public static Vector2 Rotate(Vector2 v, float degrees) {
            var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
            var tx = v.x;
            var ty = v.y;
            v.x = cos * tx - sin * ty;
            v.y = sin * tx + cos * ty;
            return v;
        }


        public static (Quaternion angle, Vector2 directionalVector) CalculateFacingToTarget(Vector2 position, Vector2 tg)
        {
            var directionalVector = (tg - position).normalized;
            var angle = Mathf.Asin(directionalVector.x / directionalVector.magnitude) * Mathf.Rad2Deg;
            if (directionalVector.y < 0)
                angle = 180 - angle;
            return (Quaternion.Euler(0, 0, -angle), directionalVector);
        }
    }
}