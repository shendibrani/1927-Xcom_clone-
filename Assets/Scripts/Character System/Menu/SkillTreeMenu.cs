using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillTreeMenu : MenuCanvas {

    [SerializeField]
    class SkillTreeDisplay
    {
        Text title;
        Button tier0Skill1;
        Button tier1Skill1;
        Button tier1Skill2;
        Button tier2Skill1;
        Button tier2Skill2;
        Button tier3Skill1;
        Button tier3Skill2;
        Button tier4Skill1;
        Button tier4Skill2;
        Button tier5Skill1;
        Button tier5Skill2;
    }

    [SerializeField]
    SkillTreeDisplay offenseTree;
    [SerializeField]
    SkillTreeDisplay defenseTree;
    [SerializeField]
    SkillTreeDisplay supportTree;

    void PopulateMenu(Character pCharacter)
    {

    }

}
