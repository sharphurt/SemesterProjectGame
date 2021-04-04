using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDeleter : MonoBehaviour
{
    private ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particles.IsAlive())
            Destroy(gameObject);
    }
}
