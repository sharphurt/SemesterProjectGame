using System;
using System.Collections;
using Abilities;
using Controllers;
using UnityEngine;
using Utils;

namespace Entities
{
    public class RaysPatternProjectile : Projectile
    {
        [Header("Bullets control")]
        public float launchPeriod;
        public int raysCount;
        public Bullet bullet;
        public float bulletSpeed;
        public float delay;

        private void Start()
        {
            StartCoroutine(ShootCoroutine());
            bullet.damage = damage;
        }

        private IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(delay);
            while (true)
            {
                ShootRays();
                yield return new WaitForSeconds(launchPeriod);
            }
        }

        public void Shoot()
        {
            var instance = Instantiate(bullet, transform.position, Quaternion.Inverse(transform.rotation));
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, rb.rotation) * bulletSpeed;
            instance.shooterCollider = shooterCollider;
            instance.shooterTag = shooterTag;
        }

        private void ShootRays()
        {
            for (var i = 0; i < raysCount; i++)
            {
                transform.Rotate(0, 0, 360f / raysCount);
                Shoot();
            }
        }
    }
}