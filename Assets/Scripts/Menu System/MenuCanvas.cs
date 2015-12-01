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
    TransitionState enterTransitionState;
    [SerializeField]
    TransitionState exitTransitionState;

    TransitionState currentState;

    public bool isActive
    {
        get;
        private set;
    }

    //used to indicate the menu manager changing to this menu, sets the active state (can be transition states)
    public virtual void setMenu()
    {
        enterTransitionState.EnterState();
        currentState = enterTransitionState;
        isActive = true;
    }

    public virtual void deselectMenu()
    {
        exitTransitionState.EnterState();
        currentState = exitTransitionState;
        isActive = false;
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
