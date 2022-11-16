using System.Collections.Generic;

    public class Vertex<T>
    {
        public T Data { get; private set; }
        public CustomDGraph<T> Graph { get; private set; }

        /// <summary>
        /// Linked list of edges that are connected to this vertex
        /// </summary>
        public CustomLinkedList<Edge<T>> OutEdges => LeavingMe();

        public CustomLinkedList<Edge<T>> InEdges => ApproachingMe();

        public bool Branches()
        {
            return OutEdges.Count > 1;
        }

        //Each Vertex need to know which other vertices it is connected to
        //Which is why it needs the graph it belongs to.
        public Vertex(T data, CustomDGraph<T> graph)
        {
            Data = data;
            Graph = graph;
        }
        
        /// <summary>
        /// Select the edges that have this vertex as the Destination
        /// </summary>
        /// <returns></returns>
        private CustomLinkedList<Edge<T>> ApproachingMe()
        {
            //Create a new list to store the edges
            var edges = new CustomLinkedList<Edge<T>>();
            //Loop through all the edges
            var currentNode = Graph.Edges.Head;
            while (currentNode != null)
            {
                //If the edge has this vertex as the destination
                if (currentNode.Data.Destination.Equals(this))
                {
                    //Add it to the list
                    edges.InsertAtTail(currentNode.Data);
                }
                currentNode = currentNode.Next;
            }
            return edges;
        }
        /// <summary>
        /// Select the edges that have this vertex as the Origin
        /// </summary>
        /// <returns></returns>
        private CustomLinkedList<Edge<T>> LeavingMe()
        {
            //Create a new list to store the edges
            var edges = new CustomLinkedList<Edge<T>>();
            //Loop through all the edges
            var currentNode = Graph.Edges.Head;
            while (currentNode != null)
            {
                //If the edge has this vertex as the origin
                if (currentNode.Data.Origin.Equals(this))
                {
                    //Add it to the list
                    edges.InsertAtTail(currentNode.Data);
                }
                currentNode = currentNode.Next;
            }
            return edges;
        }

        public override string ToString()
        {
            return $"{Data}";
        }
    }