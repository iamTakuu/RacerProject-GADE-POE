using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class AiRacerBase : MonoBehaviour, ITrackable
{
    #region VARIABLES AND PROPERTIES
    
    protected NavMeshAgent NavMeshAgent;
     private Transform targetPos;
     private WayPointManager _wayPointManager;
     private int _maxPoints;
     [SerializeField]private int _pointIndex;
     [SerializeField]private float accelerationDelay = 4f;
     [SerializeField] private TMP_Text positionText;
     public string RacerName { get; set; }
     public int Position { get; set; }
     public int WaypointsCompleted { get; set; }
     public int LapCount { get; set; }
     public float DistanceToNextPoint { get; set; }
     
     #endregion

    #region UNITY METHODS

     private void Awake()
     {
         NavMeshAgent = GetComponent<NavMeshAgent>();
         _wayPointManager = FindObjectOfType<WayPointManager>();
         _maxPoints = _wayPointManager.CountWayPoint();
         _pointIndex = 0;
         RacerName = name.Replace("(Clone)","");
     }
     private void Update()
     {
         positionText.text = Position.ToString();
     }
     private void OnEnable()
     {
         EventsManager.Instance.ActivateCar += ActivateRacer;
     }
     private void OnDisable()
     {
         EventsManager.Instance.ActivateCar -= ActivateRacer;
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
         }
         else
         {
             _pointIndex++;
         }
     }
     
     #endregion
     
    private void ActivateRacer()
    {
        StartCoroutine(ShowPositionFade());
        StartCoroutine(StartSpeedUp(accelerationDelay));
        NavMeshAgent.SetDestination(_wayPointManager.GetFirstWayPoint().position);
    }

    private IEnumerator ShowPositionFade()
    {
        positionText.DOFade(1f, 10f).SetDelay(3f);
        yield return new WaitForSeconds(10f);
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
    
    public void GetWaypointDistance()
    {
        DistanceToNextPoint = NavMeshAgent.remainingDistance;
    }
}
