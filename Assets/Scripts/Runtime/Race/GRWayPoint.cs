using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GRWayPoint : MonoBehaviour
    {
        private SphereCollider _collider;
        [SerializeField] public List<GRWayPoint> _nextWayPoints;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
        }
        // public void InitializePoint(CustomDGraph<Transform> graph)
        // {
        //     ThisVertex = new Vertex<Transform>(transform, graph);
        //     NextVertexes = new List<Vertex<Transform>>();
        //     foreach (var point in _nextWayPoints)
        //     {
        //         NextVertexes.Add(new Vertex<Transform>(point.transform, graph));
        //     }
        // }
        public Vertex<Transform> ThisVertex { get; private set; }

        public List<Vertex<Transform>> NextVertexes { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _collider.radius);
            Gizmos.color = Color.cyan;
            foreach (var point in _nextWayPoints)
            {
                Debug.DrawLine(transform.position, point.transform.position, Color.yellow);
            }
        
        }
    }
