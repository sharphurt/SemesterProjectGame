using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class GunController : MonoBehaviour
    {
        public float shootingPeriod;
        public BulletController bulletController;
        public float power;

        private GameObject shooter;
        private TargetSelectorController targetSelectorController;
        private Transform firePoint;

        private void Start()
        {
            shooter = gameObject.transform.parent.gameObject;
            firePoint = GetComponent<Transform>();
            targetSelectorController = GetComponent<TargetSelectorController>();
            StartCoroutine(nameof(DoTaskPeriodically));
        }

        private void Shoot(Transform tg)
        {
            var (facing, directionalVector) = CalculateFacingToTarget(tg);
            var instantiated = Instantiate(bulletController, firePoint.position, facing);
            var rb = instantiated.GetComponent<Rigidbody2D>();
            rb.velocity = directionalVector * power;
            instantiated.shooterCollider = shooter.GetComponent<Collider2D>();
            instantiated.shooterTag = shooter.tag;
        }

        private (Quaternion facingAngle, Vector2 directionalVector) CalculateFacingToTarget(Transform tg)
        {
            var position = firePoint.position;
            var directionalVector = (tg.position - position).normalized;
            var angle = Quaternion.LookRotation(directionalVector);
            angle.x = angle.y = 0;
            return (angle, directionalVector);
        }

        public IEnumerator DoTaskPeriodically()
        {
            while (true)
            {
                var target = targetSelectorController.FindClosestTarget();
                if (target != null)
                    Shoot(target.transform);
                yield return new WaitForSeconds(shootingPeriod);
            }
        }
    }
}