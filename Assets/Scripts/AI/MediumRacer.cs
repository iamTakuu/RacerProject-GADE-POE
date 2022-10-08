using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumRacer : AiRacerBase
{
    public override void SetStats()
    {
        NavMeshAgent.speed = 120f;
        NavMeshAgent.angularSpeed = 200f;
        NavMeshAgent.acceleration = 20f;
    }

    public override void UpdateAcceleration()
    {
        NavMeshAgent.acceleration = 130f;
    }
}
