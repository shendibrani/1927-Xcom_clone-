using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(Pawn))]
public class PawnHighlightingManager : MonoBehaviour, Targetable
{
	[SerializeField] List<Highlightable> Highlights;

	public PawnHighlightStates state { get; private set;}
	bool dirty;

	// Use this for initialization
	void Start ()
	{
		SetState (PawnHighlightStates.Deselected);
		GetComponent<Selectable> ().Deselected += OnDeselected;
		GetComponent<Selectable> ().Selected += OnSelected;
		GetComponent<GridMovementBehaviour> ().DestinationReached += UpdateNodes;
	}

	public void SetState(PawnHighlightStates pState){
		if (state == pState) {
			return;
		}
		state = pState;
		dirty = true;
	}

	void Update(){
		if (dirty) {
			Debug.Log("Clearing Dirty flag");
			UpdateHighlight ();
			dirty = false;
		}
	}

	void OnSelected()
	{
		Debug.Log ("Selected");
		SetState (PawnHighlightStates.Selected);
		UpdateNodes ();
	}
	
	void OnDeselected()
	{
		Debug.Log ("Deselected");
		SetState (PawnHighlightStates.Deselected);
		UpdateNodes ();
	}

	public void OnTargeted(Pawn targeter)
	{
		SetState (PawnHighlightStates.Targetable);
	}

	void UpdateHighlight(){

		foreach (Highlightable h in Highlights) {
			h.SetHighlight (false);
		}
		//Debug.Log ((int)state);
		if (state != PawnHighlightStates.Deselected) {
			Highlights [(int)state].SetHighlight (true);
		}

		switch (state) {
		case PawnHighlightStates.Deselected:
			foreach (PawnHighlightingManager p in FindObjectsOfType<PawnHighlightingManager>()) {
				p.SetState (PawnHighlightStates.Deselected);
			}

			break;
		case PawnHighlightStates.Selected:
			foreach (Pawn p in GetComponent<Pawn>().validTargets) {
				p.GetComponent<PawnHighlightingManager> ().OnTargeted(GetComponent<Pawn>());
			}
			break;
		}
	}

	void UpdateNodes(){
		foreach (NodeHighlightManager node in FindObjectsOfType<NodeHighlightManager>()) {
			node.SetState (NodeHighlightStates.Deselected);
		}
		if (state == PawnHighlightStates.Selected) {
			foreach (NodeBehaviour node in Pathfinder.NodesWithinSteps(GetComponent<Pawn>().currentNode, GetComponent<Pawn>().Movement)) {
				node.GetComponent<NodeHighlightManager> ().SetState (NodeHighlightStates.Reachable);
			}
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