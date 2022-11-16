using System.Collections.Generic;

//A directed graph.
    public class CustomDGraph<T>
    {
        public CustomLinkedList<Edge<T>> Edges { get; private set; }
        public CustomLinkedList<Vertex<T>> Vertices { get; private set; }

        public CustomDGraph()
        {
            Edges = new CustomLinkedList<Edge<T>>();
            Vertices = new CustomLinkedList<Vertex<T>>();
        }
        public void AddEdge(Vertex<T> start, Vertex<T> end)
        {
            var newEdge = new Edge<T>(start, end);
            if (!Vertices.Exists(start))
            {
                Vertices.InsertAtTail(start);
            }

            if (!Vertices.Exists(end))
            {
                Vertices.InsertAtTail(end);
            }

            if (!Edges.Exists(newEdge))
            {
                Edges.InsertAtTail(newEdge);
            }
        }
        
        public Vertex<T> GetFirstVertex()
        {
            return Vertices.Head.Data;
        }
        /// <summary>
        /// Returns a list of all the vertices that are adjacent to the given vertex.
        /// </summary>
        /// <param name="origin">Current Vertex</param>
        /// <returns></returns>
        public List<Vertex<T>> GetNextVertex(Vertex<T> origin)
        {
            var nextVertex = new List<Vertex<T>>();
            if (!Vertices.Exists(origin)) return nextVertex;
            //Return all the vertices that are adjacent to the given vertex.
            var current = Edges.Head;
            while (current != null)
            {
                if (current.Data.Origin.Equals(origin))
                {
                    nextVertex.Add(current.Data.Destination);
                }
                current = current.Next;
            }

            return nextVertex;
        }
        
    }