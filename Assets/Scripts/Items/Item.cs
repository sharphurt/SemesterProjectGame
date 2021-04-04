using System;
using Controllers;
using Entities;
using UnityEngine;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        public GameObject pickEffect;
        
        private void Update() =>
            transform.position += new Vector3(0, -GameManager.MovementSpeed * Time.deltaTime, 0);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PickUp(other.gameObject.GetComponent<Player>());
                Instantiate(pickEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        public abstract void PickUp(Player picker);

        private void OnBecameInvisible() => Destroy(gameObject);

    }
}