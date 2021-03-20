using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnPeriod;

    void Start()
    {
        StartCoroutine(nameof(DoTaskPeriodically));
    }

    void Update()
    {
    }

    public IEnumerator DoTaskPeriodically()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnPeriod);
        }
    }

    private void Spawn()
    {
        Debug.Log("Spawn");    
        Instantiate(enemy, gameObject.transform);
    }
}