using UnityEngine;
using Utils;

namespace Controllers
{
    public class StraightGunController : GunController
    {
        public override void Shoot()
        {
            var instance = Instantiate(bullet, transform.position, Quaternion.Inverse(transform.rotation));
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2Utils.Rotate(Vector2.up, rb.rotation) * speed;
            instance.shooterCollider = shooterCollider;
            instance.shooterTag = shooterTag;
        }
    }
}