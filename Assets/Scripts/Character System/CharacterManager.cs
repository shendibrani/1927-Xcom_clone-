using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {

    List<Character> characterList { 
        get 
        {
            return CharacterStaticStorage.instance.fullCharacterList;
        } 
    }

    //load game data in static??
    void LoadCharacters()
    {
        CharacterStaticStorage.instance.LoadFromSave("charactersave");
        //loadCharacters
    }

    void Start()
    {
        LoadCharacters();
        int i = 0;
        foreach (CharacterTabUI u in GetComponentsInChildren<CharacterTabUI>())
        {
            if (i < characterList.Count)
            {
                u.PopulateUI(characterList[i]);
                i++;
            }
        }
    }
}

public class CharacterStaticStorage
{

    public List<Character> fullCharacterList;

    public static CharacterStaticStorage instance {
		get {
			if(_instance == null){
				_instance = new CharacterStaticStorage();
			}
			return _instance;
		}
	}
	
	private static CharacterStaticStorage _instance;

    private CharacterStaticStorage() {
        fullCharacterList = new List<Character>();
    }

    //load characters into reference list from XML, pass in reference to save data
    public void LoadFromSave(string filename = "charactersave")
    {
        if (fullCharacterList == null)
        {
            fullCharacterList = new List<Character>();
        }
        XMLWriter.instance.DeserializeCharacter(filename);
        //CharacterStaticStorage.instance.LoadFromSave("defaultCharacters");
    }

    //save character list into XML
    public void SaveToFile()
    {
        XMLWriter.instance.SerializeCharacter(fullCharacterList,"charactersave");
    }

    //reset instance
    public static void ResetInstance()
    {
        _instance = null;
    }
}