using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeepState : ISpectatorState
{
    private static readonly int PeepID= Animator.StringToHash("Peep");
    public void EnterState(ISpectatorConetext context, Animator animator)
    {
        var randomTransition = Random.Range(0f, 0.3f);
        var randomOffset = Random.Range(0f, 0.5f);

        animator.CrossFade(PeepID, randomTransition, 0, randomOffset);
    }

    public void ExitState(ISpectatorConetext context, Animator animator)
    {
        animator.StopPlayback();
    }
}
