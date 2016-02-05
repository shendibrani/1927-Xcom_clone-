using UnityEngine;
using System.Collections;

public class ProgressToMenu : MonoBehaviour {

	public void load(){

		Application.LoadLevel (1);
		PlayerPrefs.SetInt ("MissionCount", PlayerPrefs.GetInt ("MissionCount"));
		foreach(Character c in CharacterStaticStorage.instance.fullCharacterList){
			c.LevelUp();}

	}
}
