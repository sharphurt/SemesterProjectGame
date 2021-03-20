using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}