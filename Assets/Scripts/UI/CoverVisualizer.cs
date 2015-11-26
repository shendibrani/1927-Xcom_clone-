using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class CoverVisualizer : MonoBehaviour 
{
	[SerializeField] Pawn pawn;
	// Use this for initialization

	[SerializeField] Sprite nocover, halfcover, fullcover;

	void Start () {
		SelectionManager.instance.SelectionChange += UpdateCoverState;
	}

	void UpdateCoverState(Selectable previous, Selectable current)
	{
		if (current != null && current.GetComponent<Pawn> () != null) {
			switch(pawn.GetCoverState(current.GetComponent<Pawn> ())){
			case CoverState.None:
				GetComponent<SpriteRenderer>().sprite = nocover;
				break;
			case CoverState.Half:
				GetComponent<SpriteRenderer>().sprite = halfcover;
				break;
			case CoverState.Full:
				GetComponent<SpriteRenderer>().sprite = fullcover;
				break;
			}
		}
	}
}
