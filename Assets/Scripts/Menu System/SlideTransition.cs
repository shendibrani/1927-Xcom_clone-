using UnityEngine;
using System.Collections;

public class SlideTransition : TransitionState
{

    [SerializeField]
    Vector3 motion;
    [SerializeField]
    float duration;

    float timer;

    public override void EnterState()
    {
        timer = Time.fixedTime;
    }
    public override void RunState()
    {
        if (Time.fixedTime < timer + duration)
        {
            owner.transform.position = owner.transform.position + (motion / duration);
        }
        else
        {
            ExitState();
        }
    }
    public override void ExitState()
    {
        owner.endTransition();
    }
}
