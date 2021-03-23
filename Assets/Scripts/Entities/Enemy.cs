using LevelData;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        private Vector3 targetPosition;
        private float movingSpeed;

        private void Update()
        {
            if (transform.position != new Vector3(targetPosition.x, targetPosition.y, 0))
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movingSpeed);
        }

        public void MoveToPosition(Vector2 targetPos, float speed)
        {
            targetPosition = targetPos;
            movingSpeed = speed;
        }
    }
}