using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StormCommand : Command {

    int actionCost = 3;
    int range = 4;

	public StormCommand (Pawn pOwner): base (pOwner){
        name = "Storm Command";
	}

	public override bool Execute ()
	{
		if (!CheckCost(actionCost) || !CheckTarget()) return false;

		//TODO: Actual implementation
		return true;
    }

	public override bool IsValidTarget (Targetable t)
	{
		NodeBehaviour n = t.GetComponent<NodeBehaviour>();
		return (n != null) && (Pathfinder.NodesWithinSteps (owner.currentNode, range).Contains (n));
	}
}
