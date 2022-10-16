using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AiRacerBase : MonoBehaviour, ITrackable
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
        RacerName = name.Replace("(Clone)","");
    }

    private void OnEnable()
    {
        EventsManager.Instance.ActivateCar += ActivateRacer;
    }

    private void OnDisable()
    {
        EventsManager.Instance.ActivateCar -= ActivateRacer;
    }
     //private void Start() => InvokeRepeating(nameof(GetWaypointDistance), 1f, 0.2f);

     private void ActivateRacer()
    {
        StartCoroutine(StartSpeedUp(accelerationDelay));
        NavMeshAgent.SetDestination(_wayPointManager.GetFirstWayPoint().position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Waypoint") return;
        WaypointsCompleted++;
        NavMeshAgent.SetDestination(_wayPointManager.GetNextWaypoint(_pointIndex).position);
        GetWaypointDistance();
        if (_pointIndex == _maxPoints - 1)
        {
            _pointIndex = 0;
            LapCount++;
            //Debug.Log($"{name} has completed Lap {LapCount}");
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

    public int WaypointsCompleted { get; set; }
    public int LapCount { get; set; }
    public float DistanceToNextPoint { get; set; }
    public void GetWaypointDistance()
    {
        DistanceToNextPoint = NavMeshAgent.remainingDistance;
    }

    public string RacerName { get; set; }
}
