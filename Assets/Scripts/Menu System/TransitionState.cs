using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MenuCanvas))]
public abstract class TransitionState : MonoBehaviour {

    protected MenuCanvas owner;

    void Start()
    {
        owner = GetComponent<MenuCanvas>();
    }

    public abstract void EnterState();
    public abstract void RunState();
    public abstract void ExitState();
}
