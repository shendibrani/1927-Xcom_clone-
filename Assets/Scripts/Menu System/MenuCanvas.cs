using UnityEngine;
using System.Collections;

public abstract class MenuCanvas : MonoBehaviour {

    [SerializeField]
    int id;

    public int GetID()
    {
        return id;
    }    

    protected MenuState currentState;

    //used to indicate the menu manager changing to this menu, sets the active state (can be transition states)
    public abstract void ActivateMenu();


    void Update()
    {
        if (currentState != null) currentState.Run();
    }
}
