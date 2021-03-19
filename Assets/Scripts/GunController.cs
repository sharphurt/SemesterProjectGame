using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float shootPeriod;
    public GameObject bullet;

    private Transform firePoint;

    void Start()
    {
        firePoint = GetComponent<Transform>();
        StartCoroutine(nameof(DoTaskPeriodically));
    }

    private IEnumerator DoTaskPeriodically()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootPeriod);
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}