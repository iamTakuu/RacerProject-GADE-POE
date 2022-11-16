using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    private CustomLinkedList<Transform> wayPointsList;

    private void Awake()
    {
        wayPointsList = new CustomLinkedList<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPointsList.InsertAtTail(transform.GetChild(i));
        }
    }

    public int CountWayPoint()
    {
        return transform.childCount;
    }

    /// <summary>
    /// Returns the waypoint after the current one.
    /// </summary>
    /// <param name="currentPoint"></param>
    /// <returns></returns>
    public Transform GetNextWaypoint(int currentPoint)
    {
        return wayPointsList.ReturnNextPoint(currentPoint);
    }

    public Transform GetFirstWayPoint()
    {
        return wayPointsList.ReturnHead();
    }
}
