﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CampaignManager
{

    //mission count first mission == 0;
    int missionCount;

    public int MissionCount
    {
        get
        {
            return missionCount;
        }
        private set
        {
            PlayerPrefs.SetInt("MissionCount", value);
            missionCount = value;
        }
    }

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

    public void NewCampaign()
    {
        CharacterStaticStorage.instance.LoadFromSave("defaultCharacters");
        CharacterStaticStorage.instance.fullCharacterList.Add(Factory.GetCharacter(CharacterClass.ASSAULT));
        CharacterStaticStorage.instance.fullCharacterList.Add(Factory.GetCharacter(CharacterClass.DEFENDER));
        CharacterStaticStorage.instance.fullCharacterList.Add(Factory.GetCharacter(CharacterClass.SUPPORT));
        CharacterStaticStorage.instance.SaveToFile();
        missionCount = 0;
    }

    public void NextMission()
    {
        missionCount++;
    }

    public void LoadCampaign()
    {
        CharacterStaticStorage.instance.LoadFromSave();
        missionCount = PlayerPrefs.GetInt("MissionCount");
    }

}
