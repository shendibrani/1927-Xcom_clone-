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

    MenuCanvas activeMenu;
    MenuCanvas previousMenu;

    [SerializeField]
    MenuCanvas[] MenuCanvasList;

    public Canvas canvasRefence { get; private set; }

    void Start()
    {
        canvasRefence = GetComponent<Canvas>();
    }

	// Update is called once per frame
	void Update () {
    }

    //called by button/states to trigger transition
    public void ChangeMenu(MenuCanvas menu)
    {
        if (activeMenu != null && activeMenu.isActive)
        {
            activeMenu.deselectMenu();
        }

        if (!menu.isActive)
        {
            menu.setMenu();
        }

        previousMenu = activeMenu;
        activeMenu = menu;
    }

    //opens a menu without closing former menus, can be used in conjunction with closeMenu
    public void OpenMenu(MenuCanvas menu)
    {
        if (!menu.isActive)
        {
            menu.setMenu();
        }

        previousMenu = activeMenu;
        activeMenu = menu;
    }

    public void CloseMenu(MenuCanvas menu)
    {
        if (menu != null && menu.isActive)
        {
            menu.deselectMenu();
        }

        activeMenu = previousMenu;
    }

    //returns to the previous active menu (only by one step) care for depth since it set the active menu/id to the previous menu
    public void PreviousMenu()
    {
        if (activeMenu != null && activeMenu.isActive)
        {
            activeMenu.deselectMenu();
        }
        if (previousMenu != null && !previousMenu.isActive)
        {
            previousMenu.setMenu();
        }
        activeMenu = previousMenu;
    }

    public void CloseAllMenus()
    {
        foreach (MenuCanvas c in MenuCanvasList)
        {
            if (c.isActive) c.deselectMenu();
        }
    }

}
