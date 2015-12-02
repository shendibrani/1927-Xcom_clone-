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
		ClearSelections ();

		if (previous != null) {
			if (previous.GetComponent<PawnHighlightingManager> () != null) {
				previous.GetComponent<PawnHighlightingManager> ().UpdateNodes ();
			}
		}

		if (current != null) {
			if (current.GetComponent<PawnHighlightingManager> () != null && TurnManager.instance.turnPlayer.Owns (current.GetComponent<Pawn> ())) {
				current.GetComponent<PawnHighlightingManager> ().SetState (PawnHighlightStates.Selected);
				current.GetComponent<PawnHighlightingManager> ().UpdateNodes ();
			}

			if (current.GetComponent<NodeHighlightManager> () != null) {
				current.GetComponent<NodeHighlightManager> ().SetState (NodeHighlightStates.Selected);
			}
		}
	}

	public void ClearSelections()
	{
		foreach (PawnHighlightingManager phm in GameObject.FindObjectsOfType<PawnHighlightingManager>()){
			phm.SetState (PawnHighlightStates.Deselected);
		}
		foreach (NodeHighlightManager nhm in GameObject.FindObjectsOfType<NodeHighlightManager>()){
			nhm.SetState (NodeHighlightStates.Deselected);
		}
	}

	public void RefreshHighlighting()
	{
		ClearSelections ();
		ComputeHighlightChange (null, SelectionManager.selected);
		ShowTargetingOptions (SelectionManager.command);
		ShowTarget (SelectionManager.command);
	}

	public void ShowTargetingOptions(Command c)
	{
		if (c != null) {
			foreach (Targetable t in c.validTargets) {
				t.IsValidTarget (c.owner);
			}
		}
	}

	public void ShowTarget(Command c)
	{
		if (c != null) {
			if (c.targetsAllValidTargets) {
				foreach (Targetable t in c.validTargets) {
					t.IsTargeted (c.owner);
				}
			} else {
				if(c.target != null) c.target.IsTargeted (c.owner);
			}
		}
	}

	
}

