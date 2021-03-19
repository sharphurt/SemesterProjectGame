using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public Vector2 direction;

    public GameObject impactEffect;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
    }

    private void OnBecameInvisible() => Destroy(gameObject);


    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(damage);

        if (impactEffect != null)
            Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}