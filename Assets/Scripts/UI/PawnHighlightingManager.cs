using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Selectable))]
public class PawnHighlightingManager : MonoBehaviour
{
	[SerializeField] List<Highlightable> Highlights;

	// Use this for initialization
	void Start ()
	{
		SetState (PawnHighlightStates.Deselected);
		GetComponent<Selectable> ().Deselected += OnDeselected;
		GetComponent<Selectable> ().Selected += OnSelected;
	}

	public void SetState(PawnHighlightStates state){
		foreach (Highlightable h in Highlights) {
			h.SetHighlight(false);
		}

		if (state == PawnHighlightStates.Deselected) {
			return;
		}
		//Debug.Log ((int)state);

		Highlights[(int)state].SetHighlight(true);
	}

	void OnSelected(){
		//Debug.Log ("Selected");
		SetState (PawnHighlightStates.Selected);
		foreach (Pawn p in GetComponent<Pawn>().validTargets){
			p.GetComponent<PawnHighlightingManager>().SetState(PawnHighlightStates.Targetable);
		}
		foreach (NodeBehaviour node in Pathfinder.NodesWithinSteps(GetComponent<Pawn>().currentNode, GetComponent<Pawn>().Movement)) {
			node.GetComponent<NodeHighlightManager>().SetState(NodeHighlightStates.Reachable);
		}
	}
	
	void OnDeselected(){
		//Debug.Log ("Deselected");
		SetState (PawnHighlightStates.Deselected);
		foreach (PawnHighlightingManager p in FindObjectsOfType<PawnHighlightingManager>()){
			p.SetState(PawnHighlightStates.Deselected);
		}
		foreach (NodeHighlightManager node in FindObjectsOfType<NodeHighlightManager>()) {
			node.SetState(NodeHighlightStates.Deselected);
		}
	}
}

public enum PawnHighlightStates
{
	Deselected = -1,
	Selected = 0,
	Targetable = 1,
	Targeted = 2
}