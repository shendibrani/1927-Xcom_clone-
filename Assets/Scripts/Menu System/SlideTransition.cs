using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlideTransition : TransitionState
{
    [SerializeField]
    List<Vector2> positions;
    [SerializeField]
    float easing = 0.2f;

    // Update is called once per frame
    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += (positions[state] - rectTransform.anchoredPosition) * easing;
    }

    public override void EnterState(int pState)
    {
        state = pState;
    }
    public override void RunState()
    {
        if (Vector2.SqrMagnitude(positions[state] - GetComponent<RectTransform>().anchoredPosition) > easing)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition += (positions[state] - rectTransform.anchoredPosition) * easing;
        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = positions[state];
            ExitState();
        }
    }
    public override void ExitState()
    {
        owner.endTransition();
    }
}
