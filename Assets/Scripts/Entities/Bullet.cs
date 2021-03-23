using UnityEngine;

namespace Entities
{
    public class Bullet : Projectile
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(shooterTag) || other.GetComponent<Bullet>() != null)
                return;

            var enemy = other.GetComponent<Entity>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            if (impactEffect != null)
                Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}