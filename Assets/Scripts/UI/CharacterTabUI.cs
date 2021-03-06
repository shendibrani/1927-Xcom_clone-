﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class CharacterTabUI : MenuCanvas {

    Character characterReference;
    Weapon WeaponRef;
    int w = 0;
    [SerializeField]
    Text buttonText;

    [System.Serializable]
    public class CharacterTabData
    {
        public Text CharacterNameText;
        public Text LevelValue;
        public Text HealthValue;
        public Text ActionPointsValue;
        public Text AccuracyValue;
        public Text RangeValue;
        public Text WeaponValue;
        public Text AttackValue;
        public Text APCostValue;
        public Text CritChanceValue;
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
        if (characterReference != null)
        {
			if(buttonText != null)
            	buttonText.text = characterReference.name;

            characterStruct.CharacterNameText.text = characterReference.name;
            characterStruct.LevelValue.text = characterReference.level.ToString();
            characterStruct.HealthValue.text = characterReference.hitPoints.ToString();
            characterStruct.ActionPointsValue.text = characterReference.actionPoints.ToString();
            characterStruct.AccuracyValue.text = characterReference.accuracy.ToString();

            WeaponRef = characterReference.assignedWeapon;
            characterStruct.WeaponValue.text = characterReference.assignedWeapon.name;
            characterStruct.RangeValue.text = characterReference.assignedWeapon.range.ToString();
            characterStruct.AttackValue.text = characterReference.assignedWeapon.damage.ToString();
            characterStruct.APCostValue.text = characterReference.assignedWeapon.actionCost.ToString();
            characterStruct.CritChanceValue.text = (int)(characterReference.assignedWeapon.criticalChance * 100) + "%";
        }
	}

    //called from generic scene manager of some sort [[TO DO]] pass in character reference
    //set all the values according to the character
    //set the weapon and skill data for mouse over popup info
    //character's assigned weapone can be changed through this
    public void PopulateUI(Character c)
    {
        Debug.Log("Populate");
        characterReference = c;

		if(buttonText != null)
        	buttonText.text = characterReference.name;

        characterStruct.CharacterNameText.text = c.name;
        characterStruct.LevelValue.text = c.level.ToString();
        characterStruct.HealthValue.text = c.hitPoints.ToString();
        characterStruct.ActionPointsValue.text = c.actionPoints.ToString();
        characterStruct.AccuracyValue.text = c.accuracy.ToString();

        WeaponRef = c.assignedWeapon;
        characterStruct.WeaponValue.text = c.assignedWeapon.name;
        characterStruct.RangeValue.text = c.assignedWeapon.range.ToString();
        characterStruct.AttackValue.text = c.assignedWeapon.damage.ToString();
        characterStruct.APCostValue.text = c.assignedWeapon.actionCost.ToString();
        characterStruct.CritChanceValue.text = (int)(c.assignedWeapon.criticalChance * 100) + "%";


    }

    //cycling only works if enums have default values
    //simply decreases (left) or increases (right) value and finds the correct weapon
    public void CycleWeapon(bool isLeft)
    {
        
        //int w = (int)WeaponRef.weaponEnum;
        Debug.Log(w);
        Debug.Log(Weapons.GetNames(typeof(Weapons)).Length);
        if (isLeft)
        {
            w--;
        }
        else if (!isLeft)
        {
            w++;
        }

        if (w < 0) w = Weapons.GetNames(typeof(Weapons)).Length;
        w = w % Weapons.GetNames(typeof(Weapons)).Length;

        characterReference.assignedWeapon = WeaponData.instance.universalWeaponList[(Weapons)w].Clone();
        WeaponRef = characterReference.assignedWeapon;
        characterStruct.WeaponValue.text = characterReference.assignedWeapon.name;
    }
}
