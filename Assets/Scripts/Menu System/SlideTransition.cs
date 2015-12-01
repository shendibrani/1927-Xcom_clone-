using UnityEngine;
using System.Collections;

public class SlideTransition : TransitionState
{

    [SerializeField]
    Vector3 motion;

    [SerializeField]
    double duration;

    double timer;

    public override void EnterState()
    {
        timer = Time.fixedTime;
    }
    public override void RunState()
    {
        if (Time.fixedTime < timer + duration)
        {
            owner.GetComponent<RectTransform>().localPosition = owner.GetComponent<RectTransform>().localPosition + (motion / (float)(duration) * Time.fixedDeltaTime);
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
