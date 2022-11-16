using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AdvancedAI_Base : MonoBehaviour
    {
        #region VARIABLES AND PROPERTIES
    
        protected NavMeshAgent NavMeshAgent;
        private Transform targetPos;
        private WayPointGraph _wpGraph;
        private int _maxPoints;
        
        private Vertex<Transform> currentVertex;
        
        [SerializeField]private float accelerationDelay = 4f;
        [SerializeField] private TMP_Text positionText;
        public string RacerName { get; set; }
        public int Position { get; set; }
        public int WaypointsCompleted { get; set; }
        public int LapCount { get; set; }
        public float DistanceToNextPoint { get; set; }
     
        #endregion

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _wpGraph = FindObjectOfType<WayPointGraph>();
            currentVertex = _wpGraph.FirstPoint();
            
            NavMeshAgent.SetDestination(currentVertex.Data.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Waypoint")) return;
            var nextPoint = _wpGraph.NextPoint(currentVertex);
            currentVertex = nextPoint;
            NavMeshAgent.SetDestination(nextPoint.Data.position);
        }
    }
