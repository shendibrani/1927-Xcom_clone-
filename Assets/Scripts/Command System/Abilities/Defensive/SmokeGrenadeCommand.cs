using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmokeGrenadeCommand : Command
{

    int range = 5;
    int areaOfEffect = 2;
    int actionCost = 2;


    public SmokeGrenadeCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Smoke Bomb Command";
    }

    public override bool Execute()
    {
        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

		NodeBehaviour targetNode = target.GetComponent<NodeBehaviour> ();

        foreach (NodeBehaviour n in Pathfinder.NodesWithinSteps(targetNode, areaOfEffect, false))
        {
            if (n.isOccupied) n.currentObject.GetComponent<Pawn>().EffectList.Add(Factory.GetEffect(Effects.BlindDebuff, owner));
        }

        Debug.Log(owner + " Executes " + name);

        return true;
    }

	public override bool IsValidTarget (Targetable t)
	{
		NodeBehaviour n = t.GetComponent<NodeBehaviour>();
		return (n != null) && (Pathfinder.NodesWithinSteps (owner.currentNode, range).Contains (n));
	}
}
