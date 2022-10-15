using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyRacer : AiRacerBase
{
    protected override void SetStats()
    {
        NavMeshAgent.speed = 118f;
        NavMeshAgent.angularSpeed = 180f;
        NavMeshAgent.acceleration = 20f;
    }

    protected override void UpdateAcceleration()
    {
        NavMeshAgent.acceleration = 130f;
    }
}
