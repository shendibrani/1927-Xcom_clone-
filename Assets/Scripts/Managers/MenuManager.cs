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
        foreach (MenuCanvas m in MenuCanvasList)
        {
            menuCanvasReference.Add(m.GetID(), m);
        }
    }

	// Update is called once per frame
	void Update () {
    }

    //called by button/states to trigger transition
    public void ChangeMenu(int id)
    {
        if (previousMenuID != null) previousMenuID = activeMenuID;
        activeMenuID = id;
        menuCanvasReference[activeMenuID].ActivateMenu();
    }
}
