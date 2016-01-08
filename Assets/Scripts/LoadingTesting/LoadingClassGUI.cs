using UnityEngine;
using System.Collections;

public class LoadingClassGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            //SerializeWeapons(weapList);
            //SkillData.instance.universalSkillList.Add(Commands.AimedAttack, new Skill("Aimed Attack", "Generic Description", Commands.AimedAttack));
            //SkillData.instance.universalSkillList.Add(Commands.Defend, new Skill("Defend", "Generic Description", Commands.Defend));
            //SkillData.instance.universalSkillList.Add(Commands.FirstAid, new Skill("First Aid", "Generic Description", Commands.FirstAid));
            //XMLWriter.instance.SerializeSkills(SkillData.instance.universalSkillList);
            CharacterStaticStorage.instance.fullCharacterList.Add(new Character(0,"Name", CharacterClass.ASSAULT, WeaponData.instance.universalWeaponList[Weapons.AssaultRifle]));
            XMLWriter.instance.SerializeCharacter(CharacterStaticStorage.instance.fullCharacterList, "testCharacter");
            //Debug.Log("Saved at: " + Application.dataPath + "testCharacter.txt");
        }

        if (GUILayout.Button("Load"))
        {
            XMLWriter.instance.DeserializeCharacter("testCharacter");
            //XMLWriter.instance.DeserializeSkills();
            // Debug.Log("Saved at: " + Application.dataPath + "mysave.txt");
        }

    }
}
