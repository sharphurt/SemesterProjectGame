using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string enemyName = nameof(Enemy);
    public int health = 100;
    public readonly int MaxHealth = 100;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}