using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardRacer : AiRacerBase
{
    protected override void SetStats()
    {
        NavMeshAgent.speed = 130f;
        NavMeshAgent.angularSpeed = 205f;
        NavMeshAgent.acceleration = 18f;
    }

    protected override void UpdateAcceleration()
    {
        NavMeshAgent.acceleration = 135f;
    }
}
