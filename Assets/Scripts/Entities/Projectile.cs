using Components.ActionComponents;
using UnityEngine;

namespace Entities
{
    public class Projectile : MonoBehaviour
    {
        public float damageCoefficient = 1;

        [HideInInspector] public float damage;
        
        [HideInInspector] public Collider2D shooterCollider;
        [HideInInspector] public string shooterTag;

        public GameObject impactEffect;

        private void OnBecameInvisible() => Destroy(gameObject);
    }
}