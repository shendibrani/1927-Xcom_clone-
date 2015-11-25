using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeHighlightManager : MonoBehaviour
{

	[SerializeField] List<Highlightable> Highlights;
	NodeHighlightStates state;
	bool dirty;

	// Use this for initialization
	void Start ()
	{
		SetState (NodeHighlightStates.Deselected);
	}

	void Update(){
		if (dirty) {
			Debug.Log("Clearing Dirty flag");
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
}

public enum NodeHighlightStates
{
	Deselected = -1,
	Selected = 0,
	Reachable = 1,
    Targetable = 2,
    Targeted = 3
}

