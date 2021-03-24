using System;
using UnityEngine;

namespace Controllers
{
    public class BackgroundMoveController : MonoBehaviour
    {
        public float scrollOffset;

        private void Update() =>
            transform.position += new Vector3(0, -GameManager.LevelMovementSpeed * Time.deltaTime, 0);

        private void LateUpdate()
        {
            var position = transform.position;
            if (Mathf.Abs(position.y) > scrollOffset)
                transform.SetPositionAndRotation(new Vector3(position.x, 0, position.z), transform.rotation);
        }
    }
}