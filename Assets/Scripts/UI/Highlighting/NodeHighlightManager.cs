using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Targetable))]
public class NodeHighlightManager : MonoBehaviour
{
	private static List<NodeHighlightManager> _all;
	
	public static List<NodeHighlightManager> all {
		get{
			if(_all == null){
				_all = new List<NodeHighlightManager>(FindObjectsOfType<NodeHighlightManager>());
			}
			return _all;
		}
	}

	[SerializeField] bool debug;

	[SerializeField] List<Highlightable> Highlights;
	public NodeHighlightStates state { get; private set; }
	bool dirty;
	
	// Use this for initialization
	void Start ()
	{
		if (_all != null) {
			_all.Add (this);
		}
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

	#region Callbacks
	
	void OnDestroy(){
		if(_all != null)
			_all.Remove(this);
	}
	
	#endregion
}

public enum NodeHighlightStates
{
	Deselected = -1,
	Selected = 0,
	Reachable = 1,
    Targetable = 2,
    Targeted = 3
}

