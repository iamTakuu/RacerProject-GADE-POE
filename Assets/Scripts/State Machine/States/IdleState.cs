using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ISpectatorState
{
    private static readonly int IdleID = Animator.StringToHash("Idle");
    public void EnterState(ISpectatorConetext context, Animator animator)
    {
        var randomTransition = Random.Range(0f, 0.4f);
        var randomOffset = Random.Range(0f, 0.8f);

        animator.CrossFade(IdleID, randomTransition, 0, randomOffset);
    }

    public void ExitState(ISpectatorConetext context, Animator animator)
    {
        animator.StopPlayback();
    }
}
