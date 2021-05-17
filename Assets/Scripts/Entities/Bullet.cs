using UnityEngine;

namespace Entities
{
    public class Bullet : Projectile
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(shooterTag) || other.GetComponent<Bullet>() != null || other.gameObject.CompareTag("Boost"))
                return;

            var entity = other.GetComponent<Entity>();
            if (entity != null)
                entity.TakeDamage(damage * damageCoefficient);

            if (impactEffect != null)
                Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}