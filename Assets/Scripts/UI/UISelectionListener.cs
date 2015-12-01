using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MenuCanvas))]
public class UISelectionListener : MonoBehaviour {

    [SerializeField]
    MenuCanvas UIMenu;

    void Start()
    {
        SelectionManager.instance.SelectionChange += instance_SelectionChange;
    }

    public void instance_SelectionChange(Selectable previous, Selectable current)
    {
        if (SelectionManager.selected != null && SelectionManager.selected.GetComponent<Pawn>() && SelectionManager.selected.GetComponent<Pawn>().owner == TurnManager.instance.turnPlayer)
        {
            if (UIMenu == null) Debug.LogError("There is no ability menu to active");
            MenuManager.instance.ChangeMenu(UIMenu);
        }
    }

    void OnDestroy()
    {
        SelectionManager.instance.SelectionChange -= instance_SelectionChange;
    }

}
