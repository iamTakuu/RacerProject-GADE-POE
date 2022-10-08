using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Node<T>
    {
        //Constructor
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
        
        //Private data with Public Property
        private T data;
        public T Data
        {
            get => data;
            set => data = value;
        }
        
        //Private Next node with Public Property
        private Node<T> next;
        public Node<T> Next
        {
            get => next;
            set => next = value;
        }

        public override string ToString()
        {
            return $"{Data}|---->|{Next}";
        }
    }
