using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillTree : MonoBehaviour
{

    public int assignedLevels { get; private set; }
    [SerializeField]
    public SkillLevel[] levelList { get; protected set; }

    //called from button press?
    public void AssignSkill(Skill pSkill)
    {
        if (levelList[assignedLevels].selectedSkill != null) Debug.LogError("skill already selected at this level");
        if (levelList[assignedLevels].skillList.Contains(pSkill)) levelList[assignedLevels].selectedSkill = pSkill;
        assignedLevels++;
    }

    public List<Skill> GetSkills()
    {
        List<Skill> tmpList = new List<Skill>();
        foreach (SkillLevel k in levelList)
        {
            if (k.selectedSkill != null) tmpList.Add(k.selectedSkill);
        }
        return tmpList;
    }

}

public class OffenseTree : SkillTree
{
    public OffenseTree()
    {
        levelList[0].skillList = new List<Skill>()
        {
            //new skill();
        };
        levelList[1].skillList = new List<Skill>(){
            //new skill();
            //new skill();
        };
        levelList[2].skillList = new List<Skill>(){
            //new skill();
            //new skill();
        };
        levelList[3].skillList = new List<Skill>(){
            //new skill();
            //new skill();
        };
        levelList[4].skillList = new List<Skill>()
        {
            //new skill();
            //new skill();
        };
        levelList[5].skillList = new List<Skill>()
        {
            //new skill();
            //new skill();
        };
    }
}

public class DefenseTree : SkillTree
{

}

public class SupportTree : SkillTree{

}

public struct SkillLevel
{
    public List<Skill> skillList;
    public Skill selectedSkill;
}

