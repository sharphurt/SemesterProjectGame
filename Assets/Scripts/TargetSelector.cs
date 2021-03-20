using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private string targetTag;

    private Transform position;

    private void Start()
    {
        position = GetComponent<Transform>();
    }
    
    public GameObject FindClosestTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        return targets.OrderBy(go => Vector3.Distance(position.position, go.transform.position)).FirstOrDefault();
    }
}