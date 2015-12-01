using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class MenuCanvas : MonoBehaviour
{

    [SerializeField]
    int id;

    public int GetID()
    {
        return id;
    }

    [SerializeField]
    TransitionState TransitionState;

    TransitionState currentState;

    public bool isActive
    {
        get;
        private set;
    }

    //used to indicate the menu manager changing to this menu, sets the active state (can be transition states)
    public virtual void setMenu()
    {
        if (TransitionState.state == 0)
        {
            TransitionState.EnterState();
            currentState = TransitionState;
            isActive = true;
        }
    }

    public virtual void deselectMenu()
    {
         if (TransitionState.state == 1)
        {
        TransitionState.EnterState();
        currentState = TransitionState;
        isActive = false;
        }
    }

    public void endTransition()
    {
        currentState = null;
    }

    void Update()
    {
        if (currentState != null) 
        {
            currentState.RunState(); 
        }
    }
}
