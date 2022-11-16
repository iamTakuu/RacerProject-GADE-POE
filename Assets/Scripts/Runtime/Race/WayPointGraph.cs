using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WayPointGraph : MonoBehaviour
    {
        private CustomDGraph<Transform> _graph = new();

        private void Awake()
        {
            transform.ForEachChildRecursive(e =>
            {
                var checkpoint = e.TryGetComponent(out GRWayPoint grWayPoint) ? grWayPoint : null;
                if (checkpoint == null) return;
            
                var vertex = new Vertex<Transform>(checkpoint.transform, _graph);
                Vertex<Transform> connectedVertex = null;

                foreach (var connectedCheckpoint in checkpoint._nextWayPoints)
                {
                    connectedVertex = new Vertex<Transform>(connectedCheckpoint.transform, _graph);
                }
                
                if (connectedVertex != null)
                {
                    _graph.AddEdge(vertex, connectedVertex);
                }
            });
        }
        
        public Vertex<Transform> FirstPoint()
        {
            return _graph.GetFirstVertex();
        }
        public Vertex<Transform> NextPoint(Vertex<Transform> currentVertex)
        {
            var nextVertices =  _graph.GetNextVertex(currentVertex);
            return nextVertices[Random.Range(0, nextVertices.Count)];
        }
    }
