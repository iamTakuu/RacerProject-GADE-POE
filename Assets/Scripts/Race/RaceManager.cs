using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    private List<ITrackable> trackableRacers;
    
    private void Start()
    {
        trackableRacers = new List<ITrackable>();
        trackableRacers = FindObjectsOfType<MonoBehaviour>().OfType<ITrackable>().ToList();
        
        InvokeRepeating(nameof(SortRacers), 5f, 1.5f);
    }

    private void SortRacers()
    {
        IEnumerable<ITrackable> orderedRacers = trackableRacers
            .OrderByDescending(racer => racer.LapCount)
            .ThenByDescending(racer => racer.WaypointsCompleted)
            .ThenBy(racer => racer.DistanceToNextPoint);

        var index = 1;
        foreach (var racer in orderedRacers)
        {
            Debug.Log($"Position {index}: {racer.RacerName}");
            index++;
        }

    }
}
