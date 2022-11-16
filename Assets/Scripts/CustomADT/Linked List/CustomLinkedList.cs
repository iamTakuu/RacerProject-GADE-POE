using UnityEngine;

public class CustomLinkedList<T>
{
        public Node<T> Head { get; private set; }
        public int Count => GetCount();
        
       //Linked list with initialised head
       public CustomLinkedList(T data)
       {
           var newNode = new Node<T>(data);
           Head = newNode;
           Head.Next = null;
       }
       //Blank linked list
       public CustomLinkedList()
       {
           Head = null;
       }

       public void InsertAtHead(T data)
       {
           //Make a new node and point it to Head
           var newNode = new Node<T>(data)
           {
               Next = Head
           };
           //Make the head the new node
           Head = newNode;
       }
       ///Use this to return the next Waypoint in the race. It returns the Head if index is the tail.
       public T ReturnNextPoint(int index)
       {
           var currentIndex = 0;
           
           //Copy the head node
           var current = Head;
           Node<T> previous = current;

           while (currentIndex != index)
           {
               //previous = current;
               current = current.Next;
               currentIndex++;
           }

           return current.Next == null ? Head.Data : current.Next.Data;
       }
       public void InsertAtTail(T data)
       {
           if (Head == null)
           {
               InsertAtHead(data);
               return;
           }
           //Make a new Node
           var newNode = new Node<T>(data);
           //Copy the head node
           var current = Head;
           
           //Loop until you find a node that points to a null.
           while (current.Next != null)
           {
               current = current.Next;
           }
           //Point that node to the new one.
           current.Next = newNode;
           
       }

       public T ReturnHead()
       {
           return Head.Data;
       }
       public bool Exists(T data)
       {
           var current = Head;
           while (current != null)
           {
               if (current.Data.Equals(data))
               {
                   return true;
               }
               current = current.Next;
           }
           return false;
       }
       /// <summary>
       /// GetCount the number of nodes in the list
       /// </summary>
       /// <returns></returns>
       private int GetCount()
       {
           if (Head == null)
           {
               return 0;
           }
           var current = Head;
           int count = 1;
           while (current.Next != null)
           {
               current = current.Next;
               count++;
           }
           return count;
       }
       
       #region UNUSED METHODS

       public void InsertAtIndex(int index, T data)
       {
           int currentIndex = 0;
           //Make a new Node
           var newNode = new Node<T>(data);
           //Copy the head node
           var current = Head;
           Node<T> previous = current;

           while (currentIndex != index)
           {
               previous = current;
               current = current.Next;
               currentIndex++;
           }
           
           previous.Next = newNode;
           newNode.Next = current;
       }

       
       public void FindData(T inData)
       {
           if (Head == null)
           {
               return;
           }
           var current = Head;
           //var previous = current;
           bool found = false;
           int index = 0;
           
           while (current.Next != null && !found)
           {
               if (current.Data.Equals(inData))
               {
                   found = true;
                   break;
               }

               current = current.Next;
               index++;
           }

           Debug.Log(found
               ? $"Successfully found '{inData}' at the index: {index}"
               : $"Could not find : {inData}");
       }

       #endregion
       
       public bool IsEmpty()
       {
           return Head == null;
       }
}

