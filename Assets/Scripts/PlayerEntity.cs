using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour, ITrackable
{
    public int WaypointsCompleted { get; set; }
    public int LapCount { get; set; }
    public float DistanceToNextPoint { get; set; }
    private Transform targetPos;
    private WayPointManager _wayPointManager;
    private int _maxPoints;
    private int _pointIndex;

    private void Awake()
    {
        _wayPointManager = FindObjectOfType<WayPointManager>();
        _maxPoints = _wayPointManager.CountWayPoint();
        RacerName = "Player";
    }
    
    //private void Start() => InvokeRepeating(nameof(GetWaypointDistance), 1f, 0.2f);

    public void GetWaypointDistance()
    {
        var nextPosition = _wayPointManager.GetNextWaypoint(_pointIndex).position;
        DistanceToNextPoint = Vector3.Distance(nextPosition, transform.position) / 2; //No clue why, but eish.
    }

    public string RacerName { get; set; }

    //public string Name { get; set; }

    private void Update()
    {
        //Debug.Log(WaypointsCompleted);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Waypoint") return;
        WaypointsCompleted++;
        GetWaypointDistance();
        if (_pointIndex == _maxPoints - 1)
        {
            _pointIndex = 0;
            LapCount++;
        }
        else
        {
            _pointIndex++;
            //Debug.Log(WaypointsCompleted);
        }
    }
}
