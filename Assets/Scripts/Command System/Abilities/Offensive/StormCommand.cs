using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StormCommand : NodeTargetingCommand {

    int actionCost = 3;
    int distance = 4;

    public override List<NodeBehaviour> validTargets
    {
        //raycast all nodes in four directions, return them as paths
       get { return Pathfinder.NodesWithinSteps(owner.currentNode, distance); }
    }

	public StormCommand (Pawn pOwner): base (pOwner){
        name = "Storm Command";
	}

	public override bool Execute (){
        return true;
    }
}
