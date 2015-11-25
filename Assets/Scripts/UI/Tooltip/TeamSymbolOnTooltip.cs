using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class TeamSymbolOnTooltip : MonoBehaviour {
	
	[SerializeField] Pawn pawn;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image> ().sprite = pawn.owner.playerSymbol;
	}
}
