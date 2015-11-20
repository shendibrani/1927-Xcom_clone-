using UnityEngine;
using System.Collections;

public abstract class TransitionState : MonoBehaviour {

    [SerializeField]
    protected MenuCanvas owner;

    public abstract void EnterState();
    public abstract void RunState();
    public abstract void ExitState();
}
