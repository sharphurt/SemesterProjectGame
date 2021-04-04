using UnityEngine;
using Utils;

namespace Controllers
{
    public class StraightGunController : GunController
    {
        protected override void Shoot()
        {
            var instance = Instantiate(ShootingAbility.bullet, transform.position, Quaternion.Inverse(transform.rotation));
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, rb.rotation) * ShootingAbility.speed;
            instance.damage = damage;
            instance.shooterCollider = shooterCollider;
            instance.shooterTag = shooterTag;
        }
    }
}