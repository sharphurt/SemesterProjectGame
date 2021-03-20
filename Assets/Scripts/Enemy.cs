using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}