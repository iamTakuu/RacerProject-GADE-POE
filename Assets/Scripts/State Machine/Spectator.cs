using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spectator : MonoBehaviour
{
    private SpectatorSM _stateMachine;
    
    private void Awake()
    {
        _stateMachine = GetComponent<SpectatorSM>();
        _stateMachine.Animator = GetComponent<Animator>();
        _stateMachine.playerEntity = FindObjectOfType<PlayerEntity>();
    }

    private void Start()
    {
        _stateMachine.UpdateState();
    }
}
