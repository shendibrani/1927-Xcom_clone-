using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CharacterTabUI : MenuCanvas {

    Character characterReference;
    Skill SkillRef;
    Weapon WeaponRef;

    [System.Serializable]
    public class CharacterTabData
    {
        public Text CharacterNameText;
        public Text CharacterClassText;
        public Text LevelValue;
        public Text HealthValue;
        public Text ActionPointsValue;
        public Text AccuracyValue;
        public Text SkillValue;
        public Text WeaponValue;
    }

    [SerializeField]
    CharacterTabData characterStruct;
	// Use this for initialization
	void Start () {
	 
	}

    public override void setMenu()
    {
        base.setMenu();
    }
	// Update is called once per frame
	void Update () {
	
	}

    //called from generic scene manager of some sort [[TO DO]] pass in character reference
    //set all the values according to the character
    //set the weapon and skill data for mouse over popup info
    //character's assigned weapone can be changed through this
    public void PopulateUI(Character c)
    {
        characterReference = c;

        characterStruct.CharacterNameText.text = c.name;
        characterStruct.CharacterClassText.text = c.characterClass.ToString();
        characterStruct.LevelValue.text = c.level.ToString();
        characterStruct.HealthValue.text = c.hitPoints.ToString();
        characterStruct.ActionPointsValue.text = c.actionPoints.ToString();
        characterStruct.AccuracyValue.text = c.accuracy.ToString();

        SkillRef = c.skillList[0];
        WeaponRef = c.assignedWeapon;
        characterStruct.SkillValue.text = c.skillList[0].name;
        characterStruct.WeaponValue.text = c.assignedWeapon.name;

    }

    //cycling only works if enums have default values
    //simply decreases (left) or increases (right) value and finds the correct weapon
    public void CycleWeapon(bool isLeft)
    {
        int w = (int)WeaponRef.weaponEnum;
        if (isLeft)
        {
            w--;
            if (w < 0)
            {
                w = Weapons.GetNames(typeof(Weapons)).Length - 1;
            }
        }
        else if (!isLeft)
        {
            w++;
            if (w > Weapons.GetNames(typeof(Weapons)).Length - 1)
            {
                w = 0;
            }
        }

        characterReference.assignedWeapon = WeaponData.instance.universalWeaponList[(Weapons)w].Clone();
        WeaponRef = characterReference.assignedWeapon;
        characterStruct.WeaponValue.text = characterReference.assignedWeapon.name;
    }
}
