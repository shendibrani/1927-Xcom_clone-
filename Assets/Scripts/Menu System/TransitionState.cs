using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MenuCanvas))]
public abstract class TransitionState : MonoBehaviour {

    protected MenuCanvas owner;

    public int state { get; protected set; }

    void Start()
    {
        owner = GetComponent<MenuCanvas>();
    }

    public abstract void EnterState(int pState);
    public abstract void RunState();
    public abstract void ExitState();
}
