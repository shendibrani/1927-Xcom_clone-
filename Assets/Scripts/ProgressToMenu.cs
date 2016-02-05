using UnityEngine;
using System.Collections;

public class ProgressToMenu : MonoBehaviour {

	public void loadWin(){

		Application.LoadLevel (1);
		PlayerPrefs.SetInt ("MissionCount", PlayerPrefs.GetInt ("MissionCount"));
		foreach(Character c in CharacterStaticStorage.instance.fullCharacterList){
			c.LevelUp();}

	}

	public void loadLoose(){
		
		Application.LoadLevel (1);
		PlayerPrefs.SetInt ("MissionCount", PlayerPrefs.GetInt ("MissionCount"));		
	}
}
