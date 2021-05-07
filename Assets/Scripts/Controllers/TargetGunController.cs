using System;
using System.Collections;
using Entities;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class TargetGunController : GunController
    {
        private TargetSelectorController targetSelectorController;
        public GameObject gunObject;

        public override void Start()
        {
            targetSelectorController = GetComponent<TargetSelectorController>();
            base.Start();
        }

        private void ShootToTarget(Transform target)
        {
            var (angle, directionalVector) = Vector2Utils.CalculateFacingToTarget(transform.position, target.position);
            var instance = Instantiate(ShootingAbility.bullet, transform.position, angle);
            var rb = instance.GetComponent<Rigidbody2D>();
            if (gunObject != null)
            {
                gunObject.transform.right = target.position - gunObject.transform.position;
                gunObject.transform.Rotate(new Vector3(0, 0, -90));
            }

            rb.velocity = directionalVector * ShootingAbility.speed;
            instance.damage = damage;
            instance.shooterCollider = shooterCollider;
            instance.shooterTag = shooterTag;
        }


        protected override void Shoot()
        {
            var target = targetSelectorController.FindClosestTarget();
            if (target != null)
                ShootToTarget(target.transform);
        }
    }
}