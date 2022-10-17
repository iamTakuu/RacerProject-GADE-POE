using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpectatorSM : MonoBehaviour, ISpectatorConetext
{
    private ISpectatorState _currentState;
    public PlayerEntity playerEntity;
    public Animator Animator;
    
    private IdleState IdleState;
    private PeepState PeepState;
    private StretchState StretchState;
    private HypeState HypeState;

    private void Awake()
    {
        IdleState = new IdleState();
        PeepState = new PeepState();
        StretchState = new StretchState();
        HypeState = new HypeState();
    }
    public void SetState(ISpectatorState newState)
    {
        _currentState?.ExitState(this, Animator);
        _currentState = newState;
        _currentState.EnterState(this, Animator);
    }
    /// <summary>
    /// Starts the Spectators State transitions
    /// </summary>
    public void UpdateState()
    {
        StartCoroutine(SpectatorBehavior());
    }
    /// <summary>
    /// Coroutine to handle the State Transitions.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpectatorBehavior()
    {
        while (!playerEntity.isRacing)
        {
            var randomState = Random.Range(0, 2);
            switch (randomState)
            {
                case 0:
                    SetState(IdleState);
                    break;
                default:
                    SetState(StretchState);
                    break;
            }
            yield return new WaitForSeconds(3f);
        }
        while(playerEntity.isRacing)
        {
            if (playerEntity.Position <= 3)
            {
                SetState(HypeState);
                yield return new WaitForSeconds(5f);
            }
            else
            {
                var watchingState = Random.Range(0, 3);
                switch (watchingState)
                {
                    case 0:
                        SetState(PeepState);
                        break;
                    case 1:
                        SetState(IdleState);
                        break;
                    default:
                        SetState(StretchState);
                        break;
                }
                yield return new WaitForSeconds(3f);
            }
        }
        
    }
}
