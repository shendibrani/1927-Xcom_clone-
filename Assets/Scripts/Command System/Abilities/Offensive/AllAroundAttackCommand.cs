using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AllAroundAttackCommand : Command
{
    Weapon weapon;
    int actionCost = 2;

    public AllAroundAttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "All Around Attack Command";
        weapon = owner.Weapon;
    }
	
    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;
        if (validTargets.Count == 0)
        {
            Debug.Log("There are no valid targets");
            return false;
        }
        if (weapon.range == 1)
        {
            foreach (Targetable t in validTargets)
            {
				Pawn p = t as Pawn;
				if(p != null){
					AttackCommand.Attack(owner, p);
				}
            }
            return true;
        }
        else
        {
            Debug.Log("Melee Weapon Not Equiped");
			foreach (Targetable t in validTargets)
			{
				Pawn p = t as Pawn;
				if(p != null){
	                LinkPositions pushDirection;
	                pushDirection = owner.currentNode.GetRelativePosition(p.currentNode);
	                NodeBehaviour tmpNode = p.currentNode.GetLinkInDirection(pushDirection);
	                if (!tmpNode.isOccupied)
	                {
	                    p.GetComponent<GridNavMeshWrapper>().currentNode = tmpNode;
	                }
				}
            }
            return true;
        }
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn x = t as Pawn;
		return (x != null) && (Vector3.Distance(owner.transform.position, x.transform.position) < 1);
	}
}
