using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WayPoint : MonoBehaviour
{
    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _collider.radius);
        
    }
}
