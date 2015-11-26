using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Skill{

    string name;
    string description;
    Commands abilityCommand;

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
    public static Dictionary<Skills, Skill> universalSkillList;

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

public enum Skills
{
    ActionBoost,
    AimedAttack,
    AllAroundAttack,
    AllOrNothing,
    BattleShout,
    FinishingAttack,
    Grenade,
    Lifesteal,
    Storm,
    SureHit,
    TripleAttack,

    Defend,
    Counter,
    Push,
    SmokeBomb,
    Recover,
    ShieldAlly,
    Stun,
    Taunt,
    NapalmBarricade,
    NuclearBlood,
    Angst,

    FirstAid,
    Medicine,
    Revive,
    AccuracyBuff,
    DefenceBuff,
    AllyActionBoost,
    HealArea,
    AbsorbShield,
    Motivate,
    Distraction,
    GroupBuff
}
