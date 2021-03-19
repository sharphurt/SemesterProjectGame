using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float shootingPeriod;
    public Gun gun;
    public Transform target;
    
    private void Start()
    {
        gun.parentCollider = GetComponentInParent<Collider2D>();
        gun.firePoint = GetComponent<Transform>();
        StartCoroutine(nameof(DoTaskPeriodically));
    }

    public IEnumerator DoTaskPeriodically()
    {
        while (true)
        {
            gun.Shoot(target);
            yield return new WaitForSeconds(shootingPeriod);
        }
    }
}