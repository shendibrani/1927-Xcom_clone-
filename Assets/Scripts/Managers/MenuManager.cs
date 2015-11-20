using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
    //Not a singleton, individual manager for each scene
    public static MenuManager menuManager
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
            Debug.Log(m);
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
        Debug.Log(menuCanvasReference[id]);
        if (previousMenuID != null)
        {
            menuCanvasReference[previousMenuID].deselectMenu();
            previousMenuID = activeMenuID; 
        }
        activeMenuID = id;
        menuCanvasReference[activeMenuID].setMenu();
    }
}
