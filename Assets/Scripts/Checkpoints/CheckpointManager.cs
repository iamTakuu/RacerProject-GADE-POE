using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private CheckpointStack _stack;
    [Header("Add checkpoints in reverse")]
    [SerializeField]private CheckPoint[] _checkPoints;
    
    [Header("Timer")]
    [SerializeField] public float timeRemaining = 10;
    [SerializeField] public bool timerOn;
    [SerializeField] public bool gameWon;
    public float timeToAdd;
    
    
    private void StartTimer()
    {
        timerOn = true;
    }

    private void Update()
    {
        if (timerOn)
        {
            if (timeRemaining > 0 && !gameWon)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerOn = false;
                EventsManager.Instance.OnDeactivateCar();
                EventsManager.Instance.OnEndGame();
            }
        }
    }
    
    private void OnEnable()
    {
        EventsManager.Instance.NextPoint += PopPoint;
        EventsManager.Instance.ActivateCar += StartTimer;
    }

    private void OnDisable()
    {
        EventsManager.Instance.NextPoint -= PopPoint;
        EventsManager.Instance.ActivateCar -= StartTimer;

    }

    private void Awake()
    {
        PushPoints();
    }

    private void Start()
    {
        FindNextPoint();
    }

    private void PushPoints()
    {
        
        _stack = new CheckpointStack(_checkPoints);

        foreach (var chPoint in _checkPoints)
        {
            _stack.Push(chPoint);
        }
    }

    private void PopPoint()
    {
        timeToAdd = _stack.Pop().timeToAdd;
        timeRemaining += timeToAdd;
        EventsManager.Instance.OnAddTime();
        FindNextPoint();
    }

    private void FindNextPoint()
    {
        if (_stack.IsEmpty())
        {
            Debug.Log("You win");
            gameWon = true;
            return;
        }
        if (_stack.Peek() == null)
        {
            return;
        }
        _stack.Peek().isTarget = true;
    }
}
