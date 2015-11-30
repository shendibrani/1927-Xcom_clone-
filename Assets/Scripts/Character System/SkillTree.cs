using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SkillTree
{

    /// <summary>
    /// Skill Tree class handles trees, which contain the skills assigned to each level of the tree and which skill is selected from each level
    /// Rudimentry functionality, can probably be done better.
    /// </summary>

    public int assignedLevels { get; private set; }
    public SkillLevel[] levelList { get; protected set; }

    public SkillTree()
    {
        levelList = new SkillLevel[6];
        assignedLevels = 1;
    }
   
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
            if (k.selectedSkill != null) tmpList.Add(k.selectedSkill.Clone());
        }
        return tmpList;
    }

    public static SkillTree CreateOffenseTree()
    {
        SkillTree tree = new SkillTree();
        tree.levelList[0].skillList.Add(SkillData.instance.universalSkillList[Skills.AllOrNothing]);
        tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Skills.SureHit]);
        tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Skills.AllAroundAttack]);
        tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Skills.Storm]);
        tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Skills.Grenade]);
        tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Skills.FinishingAttack]);
        tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Skills.AimedAttack]);
        tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Skills.BattleShout]);
        tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Skills.TripleAttack]);
        tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Skills.ActionBoost]);
        tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Skills.Lifesteal]);
        return tree;
    }

    public static SkillTree CreateDefenseTree()
    {
        SkillTree tree = new SkillTree();
        tree.levelList[0].skillList.Add(SkillData.instance.universalSkillList[Skills.Defend]);
        tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Skills.Counter]);
        tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Skills.Recover]);
        tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Skills.Push]);
        tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Skills.Taunt]);
        tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Skills.Stun]);
        tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Skills.ShieldAlly]);
        tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Skills.SmokeBomb]);
        tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Skills.NapalmBarricade]);
        tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Skills.NuclearBlood]);
        tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Skills.Angst]);
        return tree;
    }

    public static SkillTree CreateSupportTree()
    {
        SkillTree tree = new SkillTree();
        tree.levelList[0].skillList.Add(SkillData.instance.universalSkillList[Skills.FirstAid]);
        tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Skills.Medicine]);
        tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Skills.Revive]);
        tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Skills.AccuracyBuff]);
        tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Skills.DefenceBuff]);
        tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Skills.AllyActionBoost]);
        tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Skills.Motivate]);
        tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Skills.HealArea]);
        tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Skills.AbsorbShield]);
        tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Skills.Distraction]);
        tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Skills.GroupBuff]);
        return tree;
    }

}

[System.Serializable]
public struct SkillLevel
{
    public List<Skill> skillList;
    public Skill selectedSkill;
}

