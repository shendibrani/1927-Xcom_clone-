using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillTree : MonoBehaviour
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
        tree.levelList[0].skillList.Add(SkillData.universalSkillList[Skills.AllOrNothing]);
        tree.levelList[1].skillList.Add(SkillData.universalSkillList[Skills.SureHit]);
        tree.levelList[1].skillList.Add(SkillData.universalSkillList[Skills.AllAroundAttack]);
        tree.levelList[2].skillList.Add(SkillData.universalSkillList[Skills.Storm]);
        tree.levelList[2].skillList.Add(SkillData.universalSkillList[Skills.Grenade]);
        tree.levelList[3].skillList.Add(SkillData.universalSkillList[Skills.FinishingAttack]);
        tree.levelList[3].skillList.Add(SkillData.universalSkillList[Skills.AimedAttack]);
        tree.levelList[4].skillList.Add(SkillData.universalSkillList[Skills.BattleShout]);
        tree.levelList[4].skillList.Add(SkillData.universalSkillList[Skills.TripleAttack]);
        tree.levelList[5].skillList.Add(SkillData.universalSkillList[Skills.ActionBoost]);
        tree.levelList[5].skillList.Add(SkillData.universalSkillList[Skills.Lifesteal]);
        return tree;
    }

    public static SkillTree CreateDefenseTree()
    {
        SkillTree tree = new SkillTree();
        tree.levelList[0].skillList.Add(SkillData.universalSkillList[Skills.Defend]);
        tree.levelList[1].skillList.Add(SkillData.universalSkillList[Skills.Counter]);
        tree.levelList[1].skillList.Add(SkillData.universalSkillList[Skills.Recover]);
        tree.levelList[2].skillList.Add(SkillData.universalSkillList[Skills.Push]);
        tree.levelList[2].skillList.Add(SkillData.universalSkillList[Skills.Taunt]);
        tree.levelList[3].skillList.Add(SkillData.universalSkillList[Skills.Stun]);
        tree.levelList[3].skillList.Add(SkillData.universalSkillList[Skills.ShieldAlly]);
        tree.levelList[4].skillList.Add(SkillData.universalSkillList[Skills.SmokeBomb]);
        tree.levelList[4].skillList.Add(SkillData.universalSkillList[Skills.NapalmBarricade]);
        tree.levelList[5].skillList.Add(SkillData.universalSkillList[Skills.NuclearBlood]);
        tree.levelList[5].skillList.Add(SkillData.universalSkillList[Skills.Angst]);
        return tree;
    }

    public static SkillTree CreateSupportTree()
    {
        SkillTree tree = new SkillTree();
        tree.levelList[0].skillList.Add(SkillData.universalSkillList[Skills.FirstAid]);
        tree.levelList[1].skillList.Add(SkillData.universalSkillList[Skills.Medicine]);
        tree.levelList[1].skillList.Add(SkillData.universalSkillList[Skills.Revive]);
        tree.levelList[2].skillList.Add(SkillData.universalSkillList[Skills.AccuracyBuff]);
        tree.levelList[2].skillList.Add(SkillData.universalSkillList[Skills.DefenceBuff]);
        tree.levelList[3].skillList.Add(SkillData.universalSkillList[Skills.AllyActionBoost]);
        tree.levelList[3].skillList.Add(SkillData.universalSkillList[Skills.Motivate]);
        tree.levelList[4].skillList.Add(SkillData.universalSkillList[Skills.HealArea]);
        tree.levelList[4].skillList.Add(SkillData.universalSkillList[Skills.AbsorbShield]);
        tree.levelList[5].skillList.Add(SkillData.universalSkillList[Skills.Distraction]);
        tree.levelList[5].skillList.Add(SkillData.universalSkillList[Skills.GroupBuff]);
        return tree;
    }

}

public struct SkillLevel
{
    public List<Skill> skillList;
    public Skill selectedSkill;
}

