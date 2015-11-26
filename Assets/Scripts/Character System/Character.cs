using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character
{

    int ID;
    string name;
    int baseAcc = 0;
    int baseAP = 3;
    int baseHP = 10;

    public SkillTree offenseTree = new SkillTree();
    public SkillTree defenseTree = new SkillTree();
    public SkillTree supportTree = new SkillTree();

    public int accuracy
    {
        get
        {
            return (int)(baseAcc + (offenseTree.assignedLevels * (8f + 1f / 3f)) + (defenseTree.assignedLevels * (4f + 1f / 4f)) + (supportTree.assignedLevels * (4f + 1f / 4f)));
        }
    }
    public int actionPoints
    {
        get
        {
            return (int)(baseAP + (offenseTree.assignedLevels * (1f / 2f)) + (defenseTree.assignedLevels * (1f / 2f)) + (supportTree.assignedLevels * (1)));
        }
    }
    public int hitPoints
    {
        get
        {
            return (int)(baseHP + 1f/2f + (offenseTree.assignedLevels * (7 + 1f/2f)) + (defenseTree.assignedLevels * (15)) + (supportTree.assignedLevels * (7 + 1f/2f)));
        }
    }
    public int level { get; private set; }
    public int experiencePoints { get; private set; }

    public int assignedPoints
    {
        get
        {
            return offenseTree.assignedLevels + defenseTree.assignedLevels + supportTree.assignedLevels;
        }
    }

    public Weapon assignedWeapon;

    public List<Skill> skillList
    {
        get
        {
            List<Skill> tmpList = new List<Skill>();
            tmpList.AddRange(offenseTree.GetSkills());
            tmpList.AddRange(defenseTree.GetSkills());
            tmpList.AddRange(supportTree.GetSkills());
            return tmpList;
        }
    }

    //default unidentified character
    public Character()
    {
        ID = 0;
        name = "tmpName";
        assignedWeapon = WeaponData.instance.universalWeaponList[Weapons.DefaultPistol].Clone();
        //offenseTree = SkillTree.CreateOffenseTree();
        //defenseTree = SkillTree.CreateDefenseTree();
        //supportTree = SkillTree.CreateSupportTree();
    }

    public Character(int uID, string pName, SkillTree pOffense, SkillTree pDefense, SkillTree pSupport, Weapon pWeapon)
    {
        ID = uID;
        name = pName;
        offenseTree = pOffense;
        defenseTree = pDefense;
        supportTree = pSupport;
        assignedWeapon = pWeapon;
    }
}
