using LevelData;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        private Vector3 targetPosition;
        private float moveSpeed;

        private void Update()
        {
            if (transform.position != new Vector3(targetPosition.x, targetPosition.y, 0))
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }

        public void MoveTo(Vector2 targetPos, float speed)
        {
            targetPosition = targetPos;
            moveSpeed = speed;
        }
    }
}