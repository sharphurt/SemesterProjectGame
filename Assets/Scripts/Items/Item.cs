using System;
using Controllers;
using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public abstract class Item : MonoBehaviour
    {
        public GameObject pickEffect;
        private AudioSource pickupSound;

        public Sprite inventoryIcon;

        private void Start()
        {
            pickupSound = GameObject.Find("PickUpSound").GetComponent<AudioSource>();
        }

        private void Update() =>
            transform.position += new Vector3(0, -GameManager.MovementSpeed * Time.deltaTime, 0);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PickUp(other.gameObject.GetComponent<Player>());
                pickupSound.Play();
                Instantiate(pickEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        public abstract void PickUp(Player picker);

        private void OnBecameInvisible() => Destroy(gameObject);
    }
}