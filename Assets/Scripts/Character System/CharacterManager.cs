using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {

    [SerializeField]
    GameObject characterDisplayPrefab;
    List<Character> characterList { 
        get 
        {
            return CharacterStaticStorage.instance.fullCharacterList;
        } 
    }

    //load game data in static??
    void LoadCharacters()
    {
        //loadCharacters
    }
    //instantiate character prefabs
    void PopulateUI()
    {

    }

    void Start()
    {
        LoadCharacters();
        PopulateUI();
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
    public void LoadFromSave(string filename)
    {
        
    }

    //save character list into XML
    public void SaveToFile()
    {
      
    }

    //reset instance
    public static void ResetInstance()
    {
        _instance = null;
    }
}