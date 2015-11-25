using UnityEngine;
using System.Collections;

public class HighlightingManager 
{
	public static HighlightingManager instance {
		get {
			if(_instance == null){
				_instance = new HighlightingManager();
			}
			return _instance;
		}
	}
	
	private static HighlightingManager _instance;

	private HighlightingManager(){}

	public void ComputeHighlightChange(Selectable previous, Selectable current)
	{
		if (previous != null) {
			if (previous.GetComponent<PawnHighlightingManager> () != null) {
				previous.GetComponent<PawnHighlightingManager> ().SetState (PawnHighlightStates.Deselected);
				previous.GetComponent<PawnHighlightingManager> ().UpdateNodes ();
			}

			if (previous.GetComponent<NodeHighlightManager> () != null) {
				previous.GetComponent<NodeHighlightManager> ().SetState (NodeHighlightStates.Deselected);
			}
		}

		if (current.GetComponent<PawnHighlightingManager> () != null) {
			current.GetComponent<PawnHighlightingManager> ().SetState (PawnHighlightStates.Selected);
			foreach (Pawn p in current.GetComponent<Pawn>().sightList) {
				p.GetComponent<PawnHighlightingManager>().SetState(PawnHighlightStates.Targetable);
			}
			current.GetComponent<PawnHighlightingManager> ().UpdateNodes ();
		}

		if (current.GetComponent<NodeHighlightManager> () != null) {
			previous.GetComponent<NodeHighlightManager> ().SetState (NodeHighlightStates.Selected);
		}
	}
}

