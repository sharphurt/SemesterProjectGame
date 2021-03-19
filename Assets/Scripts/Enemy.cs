using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health = 100;
    public readonly int MaxHealth = 100;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(this);
    }
}