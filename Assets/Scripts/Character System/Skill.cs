using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Skill{

	public string name { get; private set; }
	public string description { get; private set; }
	public Commands abilityCommand { get; private set; }

    public Skill() { }
    public Skill(string pName, string pDescription, Commands pCommand)
    {
        name = pName;
        description = pDescription;
        abilityCommand = pCommand;
    }

    public Skill Clone()
    {
        return new Skill(name, description, abilityCommand);
    }
}

public class SkillData
{
    public Dictionary<Commands, Skill> universalSkillList;

    public static SkillData instance {
		get {
			if(_instance == null){
				_instance = new SkillData();
                _instance.LoadFromSave();
			}
			return _instance;
		}
	}

    private static SkillData _instance;

    private SkillData() { }

    //load skills from default XML (not related to save)
    public void LoadFromSave()
    {

    }
}