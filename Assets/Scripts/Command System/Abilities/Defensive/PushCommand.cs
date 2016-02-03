using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushCommand : Command
{
    int actionCost = 2;
    int distance = 3;

    public PushCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Push Command";
    }

    public override bool Execute()
    {
        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

		Pawn tPawn = target.GetComponent<Pawn>();

        if (owner.weapon.range == 1)
        {
			AttackCommand.Attack(owner, target);
        }
        
		LinkPositions pushDirection;
        pushDirection = owner.currentNode.GetRelativePositionInLinks(tPawn.currentNode);
        for (int i = 0; i < distance; i++)
        {
            NodeBehaviour tmpNode = tPawn.currentNode.GetLinkInDirection(pushDirection);
            if (!tmpNode.isOccupied)
            {
                tPawn.GetComponent<GridNavMeshWrapper>().currentNode = tmpNode;
            }
        }
        return true;
    }

	public override bool IsValidTarget (Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p != null) && (Vector3.Distance (owner.transform.position, p.transform.position) < 1);
	}
}
