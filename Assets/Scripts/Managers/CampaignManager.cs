using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignManager : MonoBehaviour {


    //mission count first mission == 0;
    public int missionCount { get; private set; }

    public static CampaignManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CampaignManager();
            }
            return _instance;
        }
    }

    private static CampaignManager _instance;

    public void NewCampaign() {
        CharacterStaticStorage.instance.LoadFromSave("defaultCharacters");
        missionCount = 0;
    }

    public void NextMission()
    {
        missionCount++;
    }



}
