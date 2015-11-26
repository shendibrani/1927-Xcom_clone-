using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class APDisplay : MonoBehaviour {

	[SerializeField] Pawn pawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "APs Left: " + pawn.ActionPoints + "/" + pawn.MaxActionPointsPerTurn;
	}
}

