using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Character
{

    public int ID { get; private set; }
    public string name { get; private set; }
    int baseAcc = 0;
    int baseAP = 3;
    int baseHP = 10;

    public int accuracy
    {
        get
        {
            switch (characterClass)
            {
                case CharacterClass.ASSAULT:
                    return (int)(baseAcc + (level * (8d + 1f / 3f)));
                case CharacterClass.DEFENDER:
                    return (int)(baseAcc + (level * (4f + 1f / 4f)));
                case CharacterClass.SUPPORT:
                    return (int)(baseAcc + (level * (4f + 1f / 4f)));
                default:
                    return baseAcc;
                    //return (int)(baseAcc + (offenseTree.assignedLevels * (8f + 1f / 3f)) + (defenseTree.assignedLevels * (4f + 1f / 4f)) + (supportTree.assignedLevels * (4f + 1f / 4f)));
            }
        }
    }
    public int actionPoints
    {
        get
        {
            switch (characterClass)
            {
                case CharacterClass.ASSAULT:
                    return (int)(baseAP + (level * (1f / 2f)));
                case CharacterClass.DEFENDER:
                    return (int)(baseAP + (level * (1f / 2f)));
                case CharacterClass.SUPPORT:
                    return (int)(baseAP + (level * (1f)));
                default:
                    return baseAP;

                    //return (int)(baseAP + (offenseTree.assignedLevels * (1f / 2f)) + (defenseTree.assignedLevels * (1f / 2f)) + (supportTree.assignedLevels * (1f)));
            }
        }
    }
    public int hitPoints
    {
        get
        {
            switch (characterClass)
            {
                case CharacterClass.ASSAULT:
                    return (int)(baseHP + +1f / 2f + (level * (7 + 1f / 2f)));
                case CharacterClass.DEFENDER:
                    return (int)(baseHP + +1f / 2f + (level * (15)));
                case CharacterClass.SUPPORT:
                    return (int)(baseHP + +1f / 2f + (level * (7 + 1f / 2f)));
                default:
                    return baseHP;

                    //return (int)(baseHP + 1f/2f + (offenseTree.assignedLevels * (7 + 1f/2f)) + (defenseTree.assignedLevels * (15)) + (supportTree.assignedLevels * (7 + 1f/2f)));
            }
        }
    }

    public int level { get; private set; }
    public int experiencePoints { get; private set; }
    public CharacterClass characterClass { get; private set; }

    public Weapons weaponEnum;

    [XmlIgnore]
    public Weapon assignedWeapon;

    [XmlIgnore]
    public List<Commands> skillEnumList
    {
        get
        {
            List<Commands> tmpList = new List<Commands>();
            switch (characterClass)
            {
                case CharacterClass.ASSAULT:
                    tmpList.Add(Commands.AimedAttack);
                    break;
                case CharacterClass.DEFENDER:
                    tmpList.Add(Commands.Defend);
                    break;
                case CharacterClass.SUPPORT:
                    tmpList.Add(Commands.FirstAid);
                    break;
                default:
                    break;
            }
            return tmpList;
        }
    }

    [XmlIgnore]
    public List<Skill> skillList
    {
        get
        {
            List<Skill> tmpList = new List<Skill>();
            switch (characterClass)
            {
                case CharacterClass.ASSAULT:
                    tmpList.Add(SkillData.instance.universalSkillList[Commands.AimedAttack]);
                    break;
                case CharacterClass.DEFENDER:
                    tmpList.Add(SkillData.instance.universalSkillList[Commands.Defend]);
                    break;
                case CharacterClass.SUPPORT:
                    tmpList.Add(SkillData.instance.universalSkillList[Commands.FirstAid]);
                    break;
                default:
                    break;
            }
            return tmpList;
        }
    }

    //default unidentified character
    public Character()
    {
        ID = 0;
        name = "tmpName";
        assignedWeapon = WeaponData.instance.universalWeaponList[Weapons.AssaultRifle].Clone();
        characterClass = CharacterClass.ASSAULT;
        level = 1;

        //offenseTree = SkillTree.CreateOffenseTree();
        //defenseTree = SkillTree.CreateDefenseTree();
        //supportTree = SkillTree.CreateSupportTree();
    }

    public Character(CharacterClass pClass, string pName, int pLevel = 1)
    {
        name = pName;
        characterClass = pClass;
        assignedWeapon = WeaponData.instance.universalWeaponList[Weapons.AssaultRifle].Clone();
        level = pLevel;
    }

    public Character(int uID, string pName, CharacterClass pClass, Weapon pWeapon, int pLevel)
    {
        ID = uID;
        name = pName;
        characterClass = pClass;
        assignedWeapon = pWeapon;
        level = pLevel;
    }

    public Character(int uID, string pName, CharacterClass pClass, Weapons pWeapon, int pLevel)
    {
        ID = uID;
        name = pName;
        characterClass = pClass;
        assignedWeapon = WeaponData.instance.universalWeaponList[pWeapon].Clone();
        level = pLevel;
    }
}

public enum CharacterClass
{
    ASSAULT,
    DEFENDER,
    SUPPORT
}
/*
public static class CharacterStats{
	Dictionary <CharacterClass, int [3]> characterStatsRef;
}
*/
