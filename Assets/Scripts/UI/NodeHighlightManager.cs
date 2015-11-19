using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeHighlightManager : MonoBehaviour
{

	[SerializeField] List<Highlightable> Highlights;
	
	// Use this for initialization
	void Start ()
	{
		SetState (NodeHighlightStates.Deselected);
		GetComponent<Selectable> ().Deselected += OnDeselected;
		GetComponent<Selectable> ().Selected += OnSelected;
	}
	
	public void SetState(NodeHighlightStates state){
		foreach (Highlightable h in Highlights) {
			h.SetHighlight(false);
		}
		
		if (state == NodeHighlightStates.Deselected) {
			return;
		}
		//Debug.Log ((int)state);
		
		Highlights[(int)state].SetHighlight(true);
	}
	
	void OnSelected(){
		//Debug.Log ("Selected");
		SetState (NodeHighlightStates.Selected);
	}
	
	void OnDeselected(){
		//Debug.Log ("Deselected");
		SetState (NodeHighlightStates.Deselected);
	}
}

public enum NodeHighlightStates
{
	Deselected = -1,
	Selected = 0,
	Reachable = 1,
}

