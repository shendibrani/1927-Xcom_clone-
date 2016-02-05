using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionMenuUI : MenuCanvas {

    public override void setMenu()
    {
        base.setMenu();
    }

    public override void deselectMenu()
    {
        base.deselectMenu();
    }

    public void QuitToMenu()
    {
        Application.LoadLevel(0);
    }

    public void RestartMission()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!this.isActive)
                MenuManager.instance.ChangeMenu(this);
            else
                MenuManager.instance.CloseMenu(this);
        }
    }
}
