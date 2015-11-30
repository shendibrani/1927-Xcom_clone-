using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushCommand : Command
{
    int actionCost = 2;
    int distance = 3;

    public PushCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Push Command";
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost) || !CheckTarget()) return false;

		Pawn tPawn = targets [0] as Pawn;

        if (owner.Weapon.range == 1)
        {
			AttackCommand.Attack(owner, tPawn);
        }
        
		LinkPositions pushDirection;
        pushDirection = owner.currentNode.GetRelativePosition(tPawn.currentNode);
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
		Pawn p = t.GetComponent<Pawn>();;
		return (p != null) && (Vector3.Distance (owner.transform.position, p.transform.position) < 1);
	}
}
