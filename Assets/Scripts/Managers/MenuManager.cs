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

    Dictionary<int, MenuCanvas> menuCanvasReference;

    void Start()
    {
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
		if (menuCanvasReference[previousMenuID] != null)
        {
            menuCanvasReference[previousMenuID].deselectMenu();
        }
		previousMenuID = activeMenuID; 
        activeMenuID = id;
        menuCanvasReference[activeMenuID].setMenu();
    }

    //returns to the previous active menu (only by one step) care for depth since it set the active menu/id to the previous menu
    public void CloseMenu(int id)
    {
        menuCanvasReference[activeMenuID].deselectMenu();
        if (menuCanvasReference[previousMenuID] != null)
        {
            activeMenuID = previousMenuID;
            previousMenuID = id;
            menuCanvasReference[activeMenuID].setMenu();
        }
    }

    public void CloseAllMenus()
    {
        previousMenuID = 0;
        menuCanvasReference[activeMenuID].deselectMenu();
    }

}
