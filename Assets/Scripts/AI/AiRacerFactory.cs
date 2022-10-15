using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRacerFactory : GenericFactory<AiRacerBase>
{
    private List<AiRacerBase> racers;
    [SerializeField] private Transform[] spawnPoints;
    private void Awake()
    {
        racers = new List<AiRacerBase>();
        for (var i = 0; i < 8; i++)
        {
            racers.Add(GetInstance(spawnPoints[i].position));
            racers[i].transform.rotation = spawnPoints[i].localRotation; //This fixes the weird rotation issue
        }
    }
}
