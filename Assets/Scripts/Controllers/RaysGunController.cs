using System;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class RaysGunController : GunController
    {
        public uint raysCount;
        
        private void ShootRay()
        {
            var instance = Instantiate(ShootingAbility.bullet, transform.position, Quaternion.Inverse(transform.rotation));
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, rb.rotation) * ShootingAbility.speed;
            instance.damage = damage;
            instance.shooterCollider = shooterCollider;
            instance.shooterTag = shooterTag;
        }

        protected override void Shoot()
        {
            for (int i = 0; i < raysCount; i++)
            {
                ShootRay();
                transform.Rotate(0, 0, 360f / raysCount);

            }
        }
    }
}