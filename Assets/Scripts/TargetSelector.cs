using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private string targetTag;

    [HideInInspector] public GameObject selectedTarget;

    private Transform position;

    private void Start()
    {
        position = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        selectedTarget = FindClosestTarget();
    }

    private GameObject FindClosestTarget()
    {
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        var d = targets.OrderBy(go => Vector3.Distance(position.position, go.transform.position)).FirstOrDefault();
        Debug.DrawLine(position.position, d.transform.position);
        return d;
    }
}