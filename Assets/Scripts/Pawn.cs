using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridMovementBehaviour))]
public class Pawn : MonoBehaviour
{
	public Command move;

	public Command attack;

	public List<Command> abilities; 

	public NodeBehaviour currentNode { 
		get { return GetComponent<GridMovementBehaviour> ().currentNode;}
	}

	public override string ToString ()
	{
		return name;
	}
}

