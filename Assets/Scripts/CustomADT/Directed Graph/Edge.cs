
    public class Edge<T>
    {
        /// <summary>
        /// The origin of the edge.
        /// </summary>
        public Vertex<T> Origin { get; private set; }
        
        /// <summary>
        /// The destination of the edge.
        /// </summary>
        public Vertex<T> Destination { get; private set; }

        public Edge(Vertex<T> origin, Vertex<T> destination)
        {
            Origin = origin;
            Destination = destination;
        }

        public override string ToString()
        {
            return $"{Origin} -> {Destination}";
        }
    }
