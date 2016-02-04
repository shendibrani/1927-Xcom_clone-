using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterNameUI : MenuCanvas {

    string tmpName;
    Gender tmpGender = Gender.MALE;

    [SerializeField]
    float easing = 0.2f;

    [SerializeField]
    Button maleButton;

    [SerializeField]
    Button femaleButton;

    public override void deselectMenu()
    {
        base.deselectMenu();
    }

    public override void setMenu()
    {
        base.setMenu();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if (tmpGender == Gender.MALE)
        {
            float tmpColor = (1f - maleButton.image.color.r) * easing;
            maleButton.image.color += new Color(tmpColor, tmpColor, tmpColor, 1);
            tmpColor = (0.4f - femaleButton.image.color.r) * easing;
            femaleButton.image.color += new Color(tmpColor, tmpColor, tmpColor, 1);
        }
        if (tmpGender == Gender.FEMALE)
        {
            float tmpColor = (1f - femaleButton.image.color.r) * easing;
            femaleButton.image.color += new Color(tmpColor, tmpColor, tmpColor, 1);
            tmpColor = (0.4f - maleButton.image.color.r) * easing;
            maleButton.image.color += new Color(tmpColor, tmpColor, tmpColor, 1);
        }
	}

    public void StartCampaign()
    {
        Debug.Log(tmpName);
        CharacterStaticStorage.instance.fullCharacterList.Add(new Character(0, tmpName, CharacterClass.ASSAULT, WeaponData.instance.universalWeaponList[Weapons.AssaultRifle], 1, tmpGender));
        CampaignManager.instance.NewCampaign();
    }

    public void SetName(string pName)
    {
        tmpName = pName;
    }

    public void SetGender(int pGender)
    {
        tmpGender = (Gender)pGender;
    }
}
