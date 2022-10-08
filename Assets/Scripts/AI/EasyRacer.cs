using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyRacer : AiRacerBase
{
    public override void SetStats()
    {
        NavMeshAgent.speed = 100f;
        NavMeshAgent.angularSpeed = 150f;
        NavMeshAgent.acceleration = 20f;
    }

    public override void UpdateAcceleration()
    {
        NavMeshAgent.acceleration = 130f;
    }
}
