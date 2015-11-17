using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	[SerializeField] bool debug;

	[SerializeField] List<Pawn> pawns;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp (1) && SelectionManager.selected != null) {
			if(SelectionManager.selected.GetComponent<Pawn>() != null){
				if (SelectionManager.hovered.GetComponent<NodeBehaviour>() != null){
					Move (SelectionManager.selected.GetComponent<Pawn>(), SelectionManager.hovered.GetComponent<NodeBehaviour>());
				}
			}
		}
        if (Input.GetKeyUp(KeyCode.Return))
        {
            VisionRangeUtility.GetPawns(pawns[0], 100);
        }
	}

	void Move(Pawn p, NodeBehaviour target)
	{
		if (pawns.Contains (p)) {
			p.move = new MoveCommand (p,target);
			if(debug) Debug.Log(p.move);
			if(p.move.Execute()){
				if(debug) Debug.Log("Pathfinder Successful");
			} else if(debug) { Debug.LogError("Pathfinder Failed");}

		}
	}
}

