using UnityEngine;
using System.Collections;

public abstract class MenuState : MonoBehaviour {

    public abstract void Run();

    protected abstract void EnterState();

    protected abstract void ExitState();
}
