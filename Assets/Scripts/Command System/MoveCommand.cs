using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCommand : Command
{
	public MoveCommand(Pawn pOwner) : base(pOwner)
	{
		name = "Move Command";
	}

	public override bool Execute ()
	{
		if (!CheckTarget ()) return false;

		NodeBehaviour tNode = target.GetComponent<NodeBehaviour>();

		List<NodeBehaviour> path = Pathfinder.GetPath (owner.currentNode, tNode);
        
		if (path.Count == 0) {
			return false;
		}

        int cost = Mathf.CeilToInt(((float)path.Count - 1f) / (float)Pawn.STEPSPERPOINT);

        if (!CheckCost(cost)) return false;

		owner.GetComponent<GridNavMeshWrapper> ().SetPath (path);
		//also send UI feedback at some point
		return true;
	}

	public override bool IsValidTarget (Targetable t)
	{
		NodeBehaviour n = t.GetComponent<NodeBehaviour>();
		return (n != null) && (Pathfinder.NodesWithinSteps (owner.currentNode, owner.Movement).Contains (n));
	}
}

