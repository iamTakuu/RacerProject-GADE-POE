using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AiRacerBase : MonoBehaviour
{
     protected NavMeshAgent NavMeshAgent;
     private Transform targetPos;
     private WayPointManager _wayPointManager;
     private int _maxPoints;
     [SerializeField]private int _pointIndex;
     [SerializeField]private float accelerationDelay = 4f;
    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _wayPointManager = FindObjectOfType<WayPointManager>();
        _maxPoints = _wayPointManager.CountWayPoint();
        _pointIndex = 0;
    }

    private void Start()
    {
        StartCoroutine(StartSpeedUp(accelerationDelay));
        NavMeshAgent.SetDestination(_wayPointManager.GetFirstWayPoint().position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Waypoint") return;
        NavMeshAgent.SetDestination(_wayPointManager.GetNextWaypoint(_pointIndex).position);
        if (_pointIndex == _maxPoints - 1)
        {
            _pointIndex = 0;
        }
        else
        {
            _pointIndex++;
        }
    }

    /// <summary>
    /// Set the Speed, Angular Speed and Acceleration.
    /// </summary>
    /// <param name="speed"></param>
    protected abstract void SetStats();

    protected abstract void UpdateAcceleration();

    private IEnumerator StartSpeedUp(float delay)
    {
        SetStats();
        yield return new WaitForSeconds(delay);
        UpdateAcceleration();
    }
}
