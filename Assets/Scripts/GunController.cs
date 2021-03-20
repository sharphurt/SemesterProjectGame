using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float shootingPeriod;
    public Bullet bullet;
    public float power;

    private GameObject shooter;
    private TargetSelector targetSelector;
    private Transform firePoint;

    private void Start()
    {
        shooter = gameObject.transform.parent.gameObject;
        firePoint = GetComponent<Transform>();
        targetSelector = GetComponent<TargetSelector>();
        StartCoroutine(nameof(DoTaskPeriodically));
    }

    private void Shoot(Transform tg)
    {
        var (facing, directionalVector) = CalculateFacingToTarget(tg);
        var instantiated = Instantiate(bullet, firePoint.position, facing);
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
            var target = targetSelector.FindClosestTarget();
            if (target != null)
                Shoot(target.transform);
            yield return new WaitForSeconds(shootingPeriod);
        }
    }
}