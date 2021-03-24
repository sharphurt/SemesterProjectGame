using System;
using System.Collections;
using Entities;
using UnityEngine;

namespace Controllers
{
    public class TargetGunController : GunController
    {
        private TargetSelectorController targetSelectorController;

        public override void Start()
        {
            targetSelectorController = GetComponent<TargetSelectorController>();
            base.Start();
        }

        private void ShootToTarget(Transform target)
        {
            var (angle, directionalVector) = CalculateFacingToTarget(target);
            var instance = Instantiate(ShootingAbility.bullet, transform.position, angle);
            var rb = instance.GetComponent<Rigidbody2D>();
            rb.velocity = directionalVector * ShootingAbility.speed;
            instance.shooterCollider = shooterCollider;
            instance.shooterTag = shooterTag;
        }

        private (Quaternion angle, Vector2 directionalVector) CalculateFacingToTarget(Transform tg)
        {
            var position = transform.position;
            var directionalVector = (tg.position - position).normalized;
            var angle = Mathf.Asin(directionalVector.x / directionalVector.magnitude) * Mathf.Rad2Deg;
            if (directionalVector.y < 0)
                angle = 180 - angle;
            return (Quaternion.Euler(0, 0, -angle), directionalVector);
        }

        public override void Shoot()
        {
            var target = targetSelectorController.FindClosestTarget();
            if (target != null)
                ShootToTarget(target.transform);
        }
    }
}