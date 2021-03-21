using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public HealthBar healthBar;

    public delegate void ObjectDestroyHandler(int gameObject);
    public event ObjectDestroyHandler OnObjectDestroy;

    public virtual void Start()
    {
        healthBar.SetHealthBar(100, 100, true);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealthBar(health, maxHealth, false);
        
        if (health <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        OnObjectDestroy?.Invoke(GetInstanceID());
    }
}