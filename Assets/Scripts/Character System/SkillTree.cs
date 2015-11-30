using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        tree.levelList[0].skillList.Add(SkillData.instance.universalSkillList[Commands.AllOrNothing]);
		tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Commands.SureHit]);
		tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Commands.AllAroundAttack]);
		tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Commands.Storm]);
		tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Commands.Grenade]);
		tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Commands.FinishingAttack]);
		tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Commands.AimedAttack]);
		tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Commands.BattleShout]);
		tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Commands.TripleAttack]);
		tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Commands.ActionBoost]);
		tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Commands.Lifesteal]);
        return tree;
    }

    public static SkillTree CreateDefenseTree()
    {
        SkillTree tree = new SkillTree();
		tree.levelList[0].skillList.Add(SkillData.instance.universalSkillList[Commands.Defend]);
		tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Commands.Counter]);
		tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Commands.Recover]);
		tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Commands.Push]);
		tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Commands.Taunt]);
		tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Commands.Stun]);
		tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Commands.ShieldAlly]);
		tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Commands.SmokeBomb]);
		tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Commands.NapalmBarricade]);
		tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Commands.NuclearBlood]);
		tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Commands.Angst]);
        return tree;
    }

    public static SkillTree CreateSupportTree()
    {
        SkillTree tree = new SkillTree();
		tree.levelList[0].skillList.Add(SkillData.instance.universalSkillList[Commands.FirstAid]);
		tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Commands.Medicine]);
		tree.levelList[1].skillList.Add(SkillData.instance.universalSkillList[Commands.Revive]);
		tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Commands.AccuracyBuff]);
		tree.levelList[2].skillList.Add(SkillData.instance.universalSkillList[Commands.DefenceBuff]);
		tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Commands.AllyActionBoost]);
		tree.levelList[3].skillList.Add(SkillData.instance.universalSkillList[Commands.Motivate]);
		tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Commands.HealArea]);
		tree.levelList[4].skillList.Add(SkillData.instance.universalSkillList[Commands.AbsorbShield]);
		tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Commands.Distraction]);
		tree.levelList[5].skillList.Add(SkillData.instance.universalSkillList[Commands.GroupBuff]);
        return tree;
    }

}

public struct SkillLevel
{
    public List<Skill> skillList;
    public Skill selectedSkill;
}

