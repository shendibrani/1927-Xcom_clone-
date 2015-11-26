using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	[SerializeField] bool debug;

	[SerializeField] List<Pawn> pawns;

    public List<Pawn> Pawns
    {
        get { return pawns; }
    }

    Command cachedCommand = null;

	public Sprite playerSymbol;

	// Use this for initialization
	void Start ()
	{
		foreach (Pawn p in pawns) {
			p.owner = this;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!TurnManager.instance.busy && TurnManager.instance.turnPlayer == this) {
			Debug.Log(this);
			if (Input.GetMouseButtonUp (1) && SelectionManager.selected != null) {
				if (SelectionManager.selected.GetComponent<Pawn> () != null) {
					if (SelectionManager.hovered.GetComponent<NodeBehaviour> () != null) {
						if(Move (SelectionManager.selected.GetComponent<Pawn> (), SelectionManager.hovered.GetComponent<NodeBehaviour> ())){
							TurnManager.instance.SetBusy();
						}
					} else if (SelectionManager.hovered.GetComponent<Pawn> () != null) {
						if(Attack (SelectionManager.selected.GetComponent<Pawn> (), SelectionManager.hovered.GetComponent<Pawn> ())){
							//TurnManager.instance.SetBusy();
						}
					}
				}
			}
		}
	}

	public bool Owns(Pawn p)
	{
		return pawns.Contains (p);
	}


	bool Move(Pawn p, NodeBehaviour target)
	{
		if (pawns.Contains (p)) {
			p.move = new MoveCommand (p,target);
			if(debug) Debug.Log(p.move);
			bool result = p.move.Execute();
			if(result){
				if(debug) Debug.Log("Pathfinder Successful");
			} else if(debug) { Debug.Log("Pathfinder Failed");}
			return result;
		}
		return false;
	}

    bool Attack(Pawn p, Pawn target)
    {
        if (pawns.Contains(p))
        {
            p.attack = new AttackCommand(p, target);
            bool result = p.attack.Execute();
            if (result)
            {
                if (debug) Debug.Log("Attack Successful");
            }
            else if (debug) { Debug.Log("Attack Failed"); }
			return result;
        }

		return false;
    }

	public void Turn()
	{
		foreach (Pawn p in pawns) {
			p.Turn ();
		}
	}
}

