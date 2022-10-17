using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeState : ISpectatorState
{
    private static readonly int HypeID = Animator.StringToHash("Hype");
    public void EnterState(ISpectatorConetext context, Animator animator)
    {
        var randomTransition = Random.Range(0f, 0.1f);
        var randomOffset = Random.Range(0f, 0.8f);

        animator.CrossFade(HypeID, randomTransition, 0, randomOffset);
    }

    public void ExitState(ISpectatorConetext context, Animator animator)
    {
        animator.StopPlayback();
    }
}
