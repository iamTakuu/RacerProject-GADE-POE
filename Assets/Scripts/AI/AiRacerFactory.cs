using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRacerFactory : GenericFactory<AiRacerBase>
{
    private List<AiRacerBase> racers;
    private void Start()
    {
        racers = new List<AiRacerBase>();
        for (var i = 0; i < 5; i++)
        {
            racers.Add(GetNewInstance());
        }
    }
}
