using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class EndTurnButton : MonoBehaviour
{
	void Start()
	{
		GetComponent<Button> ().onClick.AddListener (OnClick);
	}

	void OnClick()
	{
		Debug.Log ("End Turn");
		TurnManager.instance.NextTurn ();
	}
}

