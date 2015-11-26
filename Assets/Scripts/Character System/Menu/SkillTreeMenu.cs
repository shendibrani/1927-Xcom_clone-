using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillTreeMenu : MenuCanvas {

    [SerializeField]
    class SkillTreeDisplay
    {
        Text title;
        Button tier0Skill0;
        Button tier1Skill0;
        Button tier1Skill1;
        Button tier2Skill0;
        Button tier2Skill1;
        Button tier3Skill0;
        Button tier3Skill1;
        Button tier4Skill0;
        Button tier4Skill1;
        Button tier5Skill0;
        Button tier5Skill1;

        public void populateTree(SkillTree tree){

            //tier0Skill0 = tree.levelList[0].skillList[0];

        }
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
