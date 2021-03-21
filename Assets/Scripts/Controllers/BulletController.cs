using Entities;
using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        public int damage;

        [HideInInspector] public Collider2D shooterCollider;
        [HideInInspector] public string shooterTag;

        public GameObject impactEffect;

        private void OnBecameInvisible() => Destroy(gameObject);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(shooterTag) || other.GetComponent<BulletController>() != null)
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