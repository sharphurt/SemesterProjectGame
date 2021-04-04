using System;
using System.Collections;
using Abilities;
using Entities;
using Modifiers;
using UnityEngine;

namespace Controllers
{
    public abstract class GunController : MonoBehaviour
    {
        protected ShootingAbility ShootingAbility;

        [HideInInspector] public Collider2D shooterCollider;
        [HideInInspector] public string shooterTag;
        [HideInInspector] public float damage;

        protected abstract void Shoot();

        public virtual void Start()
        {
            var parent = gameObject.transform.parent.gameObject;
            shooterCollider = parent.GetComponent<Collider2D>();
            shooterTag = parent.tag;
            damage = parent.GetComponent<Entity>().damage;

            ShootingAbility = GetComponentInParent<ShootingAbility>();
            ShootingAbility.OnModifierAdded += ModifierChangedHandler;
            ShootingAbility.OnModifierRemoved += ModifierChangedHandler;

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

        private void ModifierChangedHandler(Modifier modifier)
        {
            if (modifier.FieldName != "ShootingPeriod")
                return;
            StopAllCoroutines();
            StartCoroutine(PeriodicallyShootCoroutine(ShootingAbility.isInfinityShooting,
                ShootingAbility.shootsCount));
        }

        private void OnDestroy()
        {
            ShootingAbility.OnModifierAdded -= ModifierChangedHandler;
            ShootingAbility.OnModifierRemoved -= ModifierChangedHandler;
        }
    }
}