using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEntity : MonoBehaviour, ITrackable
{
    public int WaypointsCompleted { get; set; }
    public int LapCount { get; set; }
    public float DistanceToNextPoint { get; set; }
    public bool isRacing;
    private Transform targetPos;
    private WayPointManager _wayPointManager;
    private int _maxPoints;
    private int _pointIndex;
    [SerializeField] private Canvas HUD;
    [SerializeField] private TMP_Text posText;
    [SerializeField] private TMP_Text lapText;

    public string RacerName { get; set; }
    public int Position { get; set; }

    private void OnEnable()
    {
        EventsManager.Instance.ActivateCar += ShowHud;
    }

    private void OnDisable()
    {
        EventsManager.Instance.ActivateCar -= ShowHud;
    }

    private void Awake()
    {
        _wayPointManager = FindObjectOfType<WayPointManager>();
        _maxPoints = _wayPointManager.CountWayPoint();
        RacerName = "Player";
    }

    private void ShowHud()
    {
        HUD.gameObject.SetActive(true);
        isRacing = true;
    }
    private void Update()
    {
        posText.text = $"{Position}/9";
    }

    public void GetWaypointDistance()
    {
        var nextPosition = _wayPointManager.GetNextWaypoint(_pointIndex).position;
        DistanceToNextPoint = Vector3.Distance(nextPosition, transform.position) / 2; //No clue why, but eish.
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
            lapText.text = $"Lap: {LapCount}/6";
        }
        else
        {
            _pointIndex++;
            //Debug.Log(WaypointsCompleted);
        }
    }
}
