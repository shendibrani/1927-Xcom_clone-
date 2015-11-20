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
				if (SelectionManager.hovered.GetComponent<NodeBehaviour>() != null)	{
					Move (SelectionManager.selected.GetComponent<Pawn>(), SelectionManager.hovered.GetComponent<NodeBehaviour>());
				} else if (SelectionManager.hovered.GetComponent<Pawn>() != null) {
					Attack(SelectionManager.selected.GetComponent<Pawn>(), SelectionManager.hovered.GetComponent<Pawn>());
				}
			}
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

    void Attack(Pawn p, Pawn target)
    {
        if (pawns.Contains(p))
        {
            p.attack = new AttackCommand(p, target);
            if (p.attack.Execute())
            {
                if (debug) Debug.Log("Attack Successful");
            }
            else if (debug) { Debug.Log("Attack Failed"); }
        }
    }
}

