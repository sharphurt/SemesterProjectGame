using System;
using System.Collections;
using Abilities;
using Entities;
using UnityEngine;

namespace Controllers
{
    public abstract class GunController : MonoBehaviour
    {
        protected ShootingAbility ShootingAbility;

        [HideInInspector] public Collider2D shooterCollider;
        [HideInInspector] public string shooterTag;

        public abstract void Shoot();

        public virtual void Start()
        {
            var parent = gameObject.transform.parent.gameObject;
            shooterCollider = parent.GetComponent<Collider2D>();
            shooterTag = parent.tag;

            ShootingAbility = GetComponentInParent<ShootingAbility>();
            ShootingAbility.OnModifierAdded += modifier => ModifierChangedHandler();
            ShootingAbility.OnModifierRemoved += modifier => ModifierChangedHandler();

            if (ShootingAbility == null)
                Debug.LogError("Using Gun without Shooting ability on parent");

            StartCoroutine(PeriodicallyShootCoroutine(ShootingAbility.isInfinityShooting, ShootingAbility.shootsCount));
        }

        private IEnumerator PeriodicallyShootCoroutine(bool isInf, uint repeats)
        {
            yield return new WaitForSeconds(ShootingAbility.startDelay);
            if (isInf)
                while (true)
                {
                    Shoot();
                    yield return new WaitForSeconds(ShootingAbility.ShootingPeriod);
                }

            for (var i = 0; i < repeats; i++)
            {
                Shoot();
                yield return new WaitForSeconds(ShootingAbility.ShootingPeriod);
            }
        }

        private void ModifierChangedHandler()
        {
            StopAllCoroutines();
            StartCoroutine(PeriodicallyShootCoroutine(ShootingAbility.isInfinityShooting,
                ShootingAbility.shootsCount));
        }
    }
}