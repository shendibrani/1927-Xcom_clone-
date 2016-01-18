using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignManager : MonoBehaviour {

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

    public void initialize()
    {
        CharacterStaticStorage.instance.LoadFromSave("defaultCharacters");
    }

}
