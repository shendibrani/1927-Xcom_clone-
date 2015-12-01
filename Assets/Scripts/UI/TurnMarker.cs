using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnMarker : MonoBehaviour 
{
	[SerializeField] Image turnMarker;
    [SerializeField] Text turnCount;
	
	// Use this for initialization
	void Start () {
		TurnManager.instance.TurnStart += UpdateTurnMarker;
		turnMarker.sprite = TurnManager.instance.turnPlayer.playerSymbol;
	}
	
	// Update is called once per frame
	void UpdateTurnMarker () 
	{
		turnMarker.sprite = TurnManager.instance.turnPlayer.playerSymbol;
        turnCount.text = TurnManager.instance.turn.ToString();
	}
}
