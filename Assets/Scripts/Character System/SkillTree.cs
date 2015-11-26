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

public struct SkillLevel
{
    public List<Skill> skillList;
    public Skill selectedSkill;
}

