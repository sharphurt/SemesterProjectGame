using System;
using UnityEngine;

namespace Entities
{
    public class LevelEndPoint : MonoBehaviour
    {
        private float moveSpeed;

        public delegate void GameWinHandler();

        public event GameWinHandler OnGameWin;

        private void FixedUpdate()
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 0.007f);
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        }

        public void MoveTo(float startSpeed)
        {
            moveSpeed = startSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                OnGameWin?.Invoke();
        }
    }
}