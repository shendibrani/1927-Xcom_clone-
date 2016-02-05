using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class XMLWriter
{

    public static XMLWriter instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new XMLWriter();
            }
            return _instance;
        }
    }

    private static XMLWriter _instance;

    Weapon thisWeapon;
    List<Weapon> weapList = new List<Weapon>();
    // Use this for initialization
    void Start()
    {
        //weapList.Add(WeaponData.instance.universalWeaponList[Weapons.DefaultPistol]);
        //weapList.Add(WeaponData.instance.universalWeaponList[Weapons.AssaultRifle]);
        //thisWeapon = WeaponData.instance.universalWeaponList[Weapons.DefaultPistol];
    }

    public void SerializeWeapons(List<Weapon> weaponInfo)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Weapon>));
        using (TextWriter writer = new StreamWriter(Application.dataPath + "/Resources/weaponinfo.txt"))
        {
            serializer.Serialize(writer, weaponInfo);
        }
    }

    public void DeserializeWeapons()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Weapon>));
        TextReader reader = new StreamReader(Application.dataPath + "/Resources/weaponinfo.txt");
        object obj = deserializer.Deserialize(reader);
        List<Weapon> XmlData = (List<Weapon>)obj;
        //Debug.Log("Weapons: " + XmlData.Count);
        for (int i = 0; i < XmlData.Count; i++)
        {
            //Debug.Log("Weapon: " + XmlData[i].name);
            if (!WeaponData.instance.universalWeaponList.ContainsKey(XmlData[i].weaponEnum)){
                WeaponData.instance.universalWeaponList.Add(XmlData[i].weaponEnum, XmlData[i]);
            }
        }
        reader.Close();
    }

    public void SerializeCharacter(List<Character> characterInfo, string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Character>));
        using (TextWriter writer = new StreamWriter(Application.dataPath + "/Resources/" + filename + ".txt"))
        {
            serializer.Serialize(writer, characterInfo);
        }
    }

    public void DeserializeCharacter(string filename)
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Character>));
        TextReader reader = new StreamReader(Application.dataPath + "/Resources/" + filename + ".txt");
        object obj = deserializer.Deserialize(reader);
        List<Character> XmlData = (List<Character>)obj;
        Debug.Log(XmlData.Count);
        for (int i = 0; i < XmlData.Count; i++)
        {
            //Debug.Log("Weapon: " + XmlData[i].name);
            //WeaponData.instance.universalWeaponList.Add(XmlData[i].weaponEnum, XmlData[i]);
            CharacterStaticStorage.instance.fullCharacterList.Add(XmlData[i]);
        }
        reader.Close();
    }

    public void SerializeSkills(Dictionary<Commands, Skill> skillList)
    {
        List<Skill> tmpList = new List<Skill>();
        foreach (Commands c in SkillData.instance.universalSkillList.Keys)
        {
            tmpList.Add(SkillData.instance.universalSkillList[c]);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Skill>));
        using (TextWriter writer = new StreamWriter(Application.dataPath + "/Resources/skillinfo.txt"))
        {
            serializer.Serialize(writer, tmpList);
        }
    }

    public void DeserializeSkills()
    {

        XmlSerializer deserializer = new XmlSerializer(typeof(List<Skill>));
        TextReader reader = new StreamReader(Application.dataPath + "/Resources/skillinfo.txt");
        object obj = deserializer.Deserialize(reader);
        List<Skill> XmlData = (List<Skill>)obj;
        Debug.Log(XmlData.Count);

        for (int i = 0; i < XmlData.Count; i++)
        {
            if (!SkillData.instance.universalSkillList.ContainsKey(XmlData[i].abilityCommand))
            {
                SkillData.instance.universalSkillList.Add(XmlData[i].abilityCommand, XmlData[i]);
            }
        }
        reader.Close();
    }


    /* void OnGUI() {
         if (GUILayout.Button("Save")) {
             //SerializeWeapons(weapList);
             CharacterStaticStorage.instance.fullCharacterList.Add(new Character());
             SerializeCharacter(CharacterStaticStorage.instance.fullCharacterList, "testCharacter");
             Debug.Log("Saved at: " + Application.dataPath + "mysave.txt");
         }

         if (GUILayout.Button("Load"))
         {
             DeserializeCharacter("testCharacter");
            // Debug.Log("Saved at: " + Application.dataPath + "mysave.txt");
         }

     }
      */
}
