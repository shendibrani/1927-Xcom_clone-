using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
    //Not a singleton, individual manager for each scene
    public static MenuManager instance
    {
        get
        {
            return FindObjectOfType<MenuManager>();
        }
    }

    int activeMenuID;
    int previousMenuID;

    [SerializeField]
    MenuCanvas[] MenuCanvasList;

    public Canvas canvasRefence { get; private set; }

    Dictionary<int, MenuCanvas> menuCanvasReference;

    void Start()
    {
        canvasRefence = GetComponent<Canvas>();
        menuCanvasReference = new Dictionary<int, MenuCanvas>();
        foreach (MenuCanvas m in MenuCanvasList)
        {
            if (menuCanvasReference.ContainsKey(m.GetID())) Debug.LogError("ID " + m.GetID() + "used by two MenuCanvas components");
            if (m.GetID() == 0) Debug.LogError("Cannot use 0 as menu ID");
            if (m != null)
            menuCanvasReference.Add(m.GetID(), m);
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeMenu(0);
        }
    }

    //called by button/states to trigger transition
    public void ChangeMenu(int id)
    {

        if (menuCanvasReference[activeMenuID] != null && menuCanvasReference[activeMenuID].isActive)
        {
            menuCanvasReference[activeMenuID].deselectMenu();
        }

        if (!menuCanvasReference[id].isActive)
        {
            menuCanvasReference[id].setMenu();
        }

        previousMenuID = activeMenuID;
        activeMenuID = id;

    }
    public void ChangeMenu(MenuCanvas menu)
    {
        ChangeMenu(menu.GetID());
    }

    //opens a menu without closing former menus, can be used in conjunction with closeMenu
    public void OpenMenu(int id)
    {
        if (!menuCanvasReference[id].isActive)
        {
            menuCanvasReference[id].setMenu();
        }

        previousMenuID = activeMenuID;
        activeMenuID = id;
    }
    public void OpenMenu(MenuCanvas menu)
    {
        OpenMenu(menu.GetID());
    }

    public void CloseMenu(int id)
    {
        if (menuCanvasReference[id] != null && menuCanvasReference[id].isActive)
        {
            menuCanvasReference[id].deselectMenu();
        }

        activeMenuID = previousMenuID;
    }
    public void CloseMenu(MenuCanvas menu)
    {
        CloseMenu(menu.GetID());
    }

    //returns to the previous active menu (only by one step) care for depth since it set the active menu/id to the previous menu
    public void PreviousMenu()
    {
        if (menuCanvasReference[activeMenuID] != null && menuCanvasReference[activeMenuID].isActive)
        {
            menuCanvasReference[activeMenuID].deselectMenu();
        }

        activeMenuID = previousMenuID;
    }

    public void CloseAllMenus()
    {
        foreach (MenuCanvas c in menuCanvasReference.Values)
        {
            if (c.isActive) c.deselectMenu();
        }
    }

}
