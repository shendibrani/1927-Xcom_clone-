using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MenuManager))]
public class UISelectionListener : MonoBehaviour {

    [SerializeField]
    MenuCanvas UIMenu;

    void Start()
    {
        SelectionManager.instance.SelectionChange += instance_SelectionChange;
    }

    void instance_SelectionChange(Selectable previous, Selectable current)
    {
        if (SelectionManager.selected.GetComponent<Pawn>())
        {
            if (UIMenu == null) Debug.LogError("There is no ability menu to active");
            MenuManager.instance.ChangeMenu(UIMenu.GetID());
        }
    }

    void OnDestroy()
    {
        SelectionManager.instance.SelectionChange -= instance_SelectionChange;
    }

}
