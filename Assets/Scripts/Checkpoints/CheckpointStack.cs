using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointStack
{
   private CheckPoint[] points;
   private int top;
   private int max;

   public CheckpointStack(CheckPoint[] checkPoints)
   {
      points = new CheckPoint[checkPoints.Length];
      top = -1;
      max = checkPoints.Length;
   }
   public void Push(CheckPoint point)
   {
      if (top == max - 1)
      {
         Debug.Log("Stack Overflow");
         return;
      }
      points[++top] = point;
   }
   public bool IsEmpty()
   {
      return top == -1;
   }
   public CheckPoint Pop()
   {
      return !IsEmpty() ? points[top--] : null;
   }

   public CheckPoint Peek()
   {
      return IsEmpty() ? null : points[top];
   }
}
