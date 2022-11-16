using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isTarget;
    [Header("Checkpoint Time Value")]
    public float timeToAdd = 1.5f;
    
    private Renderer thisRenderer;
    [SerializeField]private Color newColor = Color.green;
    //[SerializeField] public CheckPointInfo point;

    private void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
    }
    
    private void Update()
    {
        if (isTarget)
        {
            thisRenderer.material.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" || !isTarget) return;
        thisRenderer.material.color = newColor;
        EventsManager.Instance.OnNextPoint();
        isTarget = false;
    }

   
}
