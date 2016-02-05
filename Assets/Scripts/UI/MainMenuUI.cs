using UnityEngine;
using System.Collections;

public class MainMenuUI : MenuCanvas {

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
        CharacterStaticStorage.instance.ClearList();
        CharacterStaticStorage.instance.LoadFromSave();
        //Application.LoadLevel(PlayerPrefs.GetInt("MissionCount"));
        FindObjectOfType<LoadStuff>().OnUse(PlayerPrefs.GetInt("MissionCount"));
    }

    public void QuitToDesktop()
    {

    }
}
