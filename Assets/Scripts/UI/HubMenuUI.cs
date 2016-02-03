using UnityEngine;
using System.Collections;

public class HubMenuUI : MenuCanvas {

    public override void setMenu()
    {
        base.setMenu();
    }

    public override void deselectMenu()
    {
        base.deselectMenu();
    }

    public void LoadButton()
    {

    }

    public void QuitToMenu()
    {

    }

    public void StartMission()
    {
        int mNum = CampaignManager.instance.MissionCount + 2;
        FindObjectOfType<LoadStuff>().OnUse(mNum);
    }

    public void QuitToDesktop()
    {

    }
}
