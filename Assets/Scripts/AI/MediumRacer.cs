using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumRacer : AiRacerBase
{
    protected override void SetStats()
    {
        NavMeshAgent.speed = 125f;
        NavMeshAgent.angularSpeed = 200f;
        NavMeshAgent.acceleration = 20f;
    }

    protected override void UpdateAcceleration()
    {
        NavMeshAgent.acceleration = 130f;
    }
}
