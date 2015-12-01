using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MenuCanvas))]
public class UISelectionListener : MonoBehaviour {

    [SerializeField]
    MenuCanvas UIMenu;

    void Start()
    {
        SelectionManager.instance.SelectionChange += OnSelectionChange;
    }

    public void OnSelectionChange(Selectable previous, Selectable current)
    {
        Debug.Log("Pawn Player: " + current.GetComponent<Pawn>().owner);
        Debug.Log("Current Player: " + TurnManager.instance.turnPlayer);
        if (current != null && current.GetComponent<Pawn>() && current.GetComponent<Pawn>().owner == TurnManager.instance.turnPlayer)
        {
            //if (UIMenu == null) Debug.LogError("There is no ability menu to active");
            Debug.Log("Ability Menu Selected");
            MenuManager.instance.OpenMenu(UIMenu);
        }
        else
        {
            MenuManager.instance.CloseMenu(UIMenu);
        }
    }

    void OnDestroy()
    {
        SelectionManager.instance.SelectionChange -= OnSelectionChange;
    }

}
