using System;
using Controllers;
using UnityEngine;

namespace Entities
{
    public class LevelEndPoint : MonoBehaviour
    {
        public delegate void GameWinHandler();

        public event GameWinHandler OnGameWin;

        private void Update() =>
            transform.position += new Vector3(0, -GameManager.MovementSpeed * Time.deltaTime, 0);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                OnGameWin?.Invoke();
        }
    }
}