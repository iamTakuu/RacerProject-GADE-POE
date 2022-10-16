using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackable
{
    int WaypointsCompleted { get; set; }
    int LapCount { get; set; }
    float DistanceToNextPoint { get; set; }
    void GetWaypointDistance();
    string RacerName { get; set; }
}
