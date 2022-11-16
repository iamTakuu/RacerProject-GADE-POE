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
        NameSort();
        InvokeRepeating(nameof(SortRacers), 3f, 0.2f);
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
            racer.Position = index;
            index++;
        }
    }
    private void NameSort()
    {
        IEnumerable<ITrackable> orderedRacers = trackableRacers
            .OrderByDescending(racer => racer.RacerName);

        var index = 1;
        foreach (var racer in orderedRacers)
        {
            racer.Position = index;
            index++;
        }
    }
}
