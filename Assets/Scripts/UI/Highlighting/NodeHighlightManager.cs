using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NodeBehaviour))]
public class NodeHighlightManager : MonoBehaviour
{
	[SerializeField] bool debug;

	[SerializeField] List<Highlightable> Highlights;
	NodeHighlightStates state;
	bool dirty;
	
	// Use this for initialization
	void Start ()
	{
		SetState (NodeHighlightStates.Deselected);
		GetComponent<Targetable> ().IsTargeted += OnTargeted;
		GetComponent<Targetable> ().IsValidTarget += OnValidTarget;
		GetComponent<Targetable> ().NotTarget += OnNotTarget;
	}

	void Update(){
		if (dirty) {
			if (debug) Debug.Log("Clearing Dirty flag");
			UpdateHighlight ();
			dirty = false;
		}
	}

	public void SetState(NodeHighlightStates pState){
		if (state == pState) {
			return;
		}
		state = pState;
		dirty = true;
	}

	void UpdateHighlight()
	{
		foreach (Highlightable h in Highlights) {
			h.SetHighlight (false);
		}
		
		if (state == NodeHighlightStates.Deselected) {
			return;
		}
		//Debug.Log ((int)state);
		
		Highlights [(int)state].SetHighlight (true);
	}

	void OnValidTarget(Pawn p)
	{
		SetState(NodeHighlightStates.Targetable);
	}

	void OnTargeted(Pawn p)
	{
		SetState(NodeHighlightStates.Targeted);
	}

	void OnNotTarget(Pawn p)
	{
		SetState(NodeHighlightStates.Deselected);
	}
}

public enum NodeHighlightStates
{
	Deselected = -1,
	Selected = 0,
	Reachable = 1,
    Targetable = 2,
    Targeted = 3
}

