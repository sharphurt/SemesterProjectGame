using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Entities
{
    public class RaysPatternProjectile : Projectile
    {
        public float launchPeriod;
        public int raysCount;
        public StraightGunController straightGun;

        private void Start() => StartCoroutine(ShootCoroutine());

        private IEnumerator ShootCoroutine()
        {
            while (true)
            {
                ShootRays();
                yield return new WaitForSeconds(launchPeriod);
            }
        }

        private void ShootRays()
        {
            for (var i = 0; i < raysCount; i++)
            {
                straightGun.shooterCollider = shooterCollider;
                straightGun.shooterTag = shooterTag;
                straightGun.transform.Rotate(0, 0, 360f / raysCount);
                straightGun.Shoot();
            }
        }
    }
}