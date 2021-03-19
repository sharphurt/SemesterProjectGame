using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    [HideInInspector] public Collider2D parentCollider;

    public GameObject impactEffect;

    private void OnBecameInvisible() => Destroy(gameObject);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetInstanceID() == parentCollider.GetInstanceID())
            return;
        
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(damage);

        if (impactEffect != null)
            Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}