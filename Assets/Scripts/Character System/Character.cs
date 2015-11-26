using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character
{

    int ID;
    string name;
    public int accuracy { get; private set; }
    public int actionPoints { get; private set; }
    public int hitPoints { get; private set; }
    public int level { get; private set; }
    public int experiencePoints { get; private set;}

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

    SkillTree offenseTree;
    SkillTree defenseTree;
    SkillTree supportTree;

}
