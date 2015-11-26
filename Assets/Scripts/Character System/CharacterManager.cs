using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {

    [SerializeField]
    GameObject characterDisplayPrefab;
    List<Character> characterList;

    //load game data
    void LoadCharacters()
    {

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
