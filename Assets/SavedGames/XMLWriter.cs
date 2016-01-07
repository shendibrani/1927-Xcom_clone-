using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class XMLWriter : MonoBehaviour
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
        using (TextWriter writer = new StreamWriter(Application.dataPath + "weaponinfo.txt"))
        {
            serializer.Serialize(writer, weaponInfo);
        }
    }

    public void DeserializeWeapons()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Weapon>));
        TextReader reader = new StreamReader(Application.dataPath + "weaponinfo.txt");
        object obj = deserializer.Deserialize(reader);
        List<Weapon> XmlData = (List<Weapon>)obj;
        Debug.Log(XmlData.Count);
        for (int i = 0; i < XmlData.Count; i++)
        {
            //Debug.Log("Weapon: " + XmlData[i].name);
            WeaponData.instance.universalWeaponList.Add(XmlData[i].weaponEnum, XmlData[i]);
        }
        reader.Close();
    }

    public void SerializeCharacter(List<Character> characterInfo, string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Character>));
        using (TextWriter writer = new StreamWriter(Application.dataPath + filename + ".txt"))
        {
            serializer.Serialize(writer, characterInfo);
        }
    }

    public void DeserializeWeapons(string filename)
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Character>));
        TextReader reader = new StreamReader(Application.dataPath + filename + ".txt");
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

    /*
    void OnGUI() {
        if (GUILayout.Button("Save")) {
            SerializeWeapons(weapList);
            Debug.Log("Saved at: " + Application.dataPath + "mysave.txt");
        }

        if (GUILayout.Button("Load"))
        {
            DeserializeWeapons();
           // Debug.Log("Saved at: " + Application.dataPath + "mysave.txt");
        }

    }
     */
}
