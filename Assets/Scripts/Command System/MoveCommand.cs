using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCommand : Command
{
	NodeBehaviour target;

	NodeBehaviour originalPosition;

	public MoveCommand(Pawn pOwner, NodeBehaviour pTarget) : base(pOwner)
	{
		name = "Move Command";
		target = pTarget;
		originalPosition = pOwner.currentNode;
	}

	public override bool Execute ()
	{
		List<NodeBehaviour> path = Pathfinder.GetPath (owner.currentNode, target);
		if (path == null || path.Count == 0) {
			//also send UI feedback at some point
			return false;
		}
		owner.GetComponent<GridMovementBehaviour> ().SetPath (path);
		//also send UI feedback at some point
		return true;
	}

	public override bool Undo ()
	{
		List<NodeBehaviour> path = Pathfinder.GetPath (owner.currentNode, originalPosition);
		if (path == null || path.Count == 0) {
			//also send UI feedback at some point
			return false;
		}
		owner.GetComponent<GridMovementBehaviour> ().SetPath (path);
		//also send UI feedback at some point
		return true;
	}
}

