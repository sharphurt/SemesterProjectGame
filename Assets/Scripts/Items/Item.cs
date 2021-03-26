using System;
using Controllers;
using Entities;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        private void Update() =>
            transform.position += new Vector3(0, -GameManager.MovementSpeed * Time.deltaTime, 0);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PickUp(other.gameObject.GetComponent<Player>());
                Destroy(gameObject);
            }
        }

        public abstract void PickUp(Player picker);

        public void InstantItem(Transform position) => Instantiate(gameObject, position);
    }
}