using System.Collections;
using System.Diagnostics;
using Entities;
using UnityEngine;
using UnityEditor;
using Utils;

namespace Controllers
{
    public abstract class GunController : MonoBehaviour
    {
        public Projectile bullet;
        public float speed;
        public float startDelay;
        public float shootingPeriod;
        public bool isInfinityShooting;

        [ConditionalHide("isInfinityShooting", true)]
        public uint shootsCount;
        
        [HideInInspector] public Collider2D shooterCollider;
        [HideInInspector] public string shooterTag;

        public virtual void Start()
        {
            var parent = gameObject.transform.parent.gameObject;
            shooterCollider = parent.GetComponent<Collider2D>();
            shooterTag = parent.tag;
            StartCoroutine(PeriodicallyShootCoroutine(isInfinityShooting, shootsCount));
        }

        public abstract void Shoot();

        private IEnumerator PeriodicallyShootCoroutine(bool isInf, uint repeats)
        {
            yield return new WaitForSeconds(startDelay);
            if (isInf)
                while (true)
                {
                    Shoot();
                    yield return new WaitForSeconds(shootingPeriod);
                }

            for (var i = 0; i < repeats; i++)
            {
                Shoot();
                yield return new WaitForSeconds(shootingPeriod);
            }
        }
    }
}