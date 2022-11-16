using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpectatorState
{
    /// <summary>
    /// Use this to initially Enter a state. Cleans up any current state.
    /// </summary>
    /// <param name="context"></param>
    public void EnterState(ISpectatorConetext context, Animator animator);
    /// <summary>
    /// Exits the current state, done before entering another state.
    /// </summary>
    /// <param name="context"></param>
    public void ExitState(ISpectatorConetext context, Animator animator);
    

}
