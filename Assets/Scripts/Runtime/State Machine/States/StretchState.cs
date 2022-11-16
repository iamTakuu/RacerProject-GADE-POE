using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchState : ISpectatorState
{
    private static readonly int StretchID = Animator.StringToHash("Stretch");
    public void EnterState(ISpectatorConetext context, Animator animator)
    {
        var randomTransition = Random.Range(0f, 0.4f);
        var randomOffset = Random.Range(0f, 0.8f);

        animator.CrossFade(StretchID, randomTransition, 0, randomOffset);
    }

    public void ExitState(ISpectatorConetext context, Animator animator)
    {
        animator.StopPlayback();
    }
}
