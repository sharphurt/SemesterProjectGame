using UnityEngine;

namespace Entities
{
    public class Projectile : MonoBehaviour
    {
        public int damage;
        
        [HideInInspector] public Collider2D shooterCollider;
        [HideInInspector] public string shooterTag;

        public GameObject impactEffect;

        private void OnBecameInvisible() => Destroy(gameObject);
    }
}