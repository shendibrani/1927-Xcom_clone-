using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Targetable))]
[RequireComponent(typeof(Pawn))]
public class PawnHighlightingManager : MonoBehaviour
{
	[SerializeField] bool debug;

	[SerializeField] List<Highlightable> Highlights;

	public PawnHighlightStates state { get; private set;}
	bool dirty;

	// Use this for initialization
	void Start ()
	{
		SetState (PawnHighlightStates.Deselected);
		GetComponent<GridNavMeshWrapper> ().DestinationReached += UpdateNodes;
		GetComponent<Targetable> ().IsValidTarget += OnValidTarget;
		GetComponent<Targetable> ().IsTargeted += OnTargeted;
		GetComponent<Targetable> ().NotTarget += OnNotTarget;
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
			if (debug) Debug.Log("Clearing Dirty flag");
			UpdateHighlight();
			dirty = false;
		}
	}

	void UpdateHighlight()
	{
		foreach (Highlightable h in Highlights) {
			h.SetHighlight (false);
		}
		//Debug.Log ((int)state);
		if (state != PawnHighlightStates.Deselected) {
			Highlights [(int)state].SetHighlight (true);
		}
	}

	public void UpdateNodes()
	{
		foreach (NodeHighlightManager node in FindObjectsOfType<NodeHighlightManager>()) {
			node.SetState (NodeHighlightStates.Deselected);
		}
		if (state == PawnHighlightStates.Selected) {
			foreach (NodeBehaviour node in Pathfinder.NodesWithinSteps(GetComponent<Pawn>().currentNode, GetComponent<Pawn>().Movement)) {
				node.GetComponent<NodeHighlightManager> ().SetState (NodeHighlightStates.Reachable);
			}
		}
	}

	void OnValidTarget(Pawn p)
	{
		SetState(PawnHighlightStates.Targetable);
	}

	void OnTargeted (Pawn targeter)
	{
		SetState (PawnHighlightStates.Targetable);
	}

	void OnNotTarget (Pawn targeter)
	{
		SetState (PawnHighlightStates.Deselected);
	}
}

public enum PawnHighlightStates
{
	Deselected = -1,
	Selected = 0,
	Targetable = 1,
	Targeted = 2
}