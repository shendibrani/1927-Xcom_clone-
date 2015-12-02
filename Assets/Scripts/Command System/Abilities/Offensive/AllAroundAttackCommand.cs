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
        targetsAllValidTargets = true;
    }
	
    public override bool Execute()
    {
        if (!CheckCost(actionCost))
            return false;

        if (validTargets.Count == 0)
        {
            Debug.Log("There are no valid targets");
            return false;
        }

        if (weapon.range == 1)
        {
            foreach (Targetable t in validTargets)
            {
					AttackCommand.Attack(owner, t.GetComponent<Pawn>());
            }
        }
        else
        {
            Debug.Log("Melee Weapon Not Equiped");
			foreach (Targetable t in validTargets)
			{
	                LinkPositions pushDirection;
	                pushDirection = owner.currentNode.GetRelativePosition(t.GetComponent<Pawn>().currentNode);
	                NodeBehaviour tmpNode = t.GetComponent<Pawn>().currentNode.GetLinkInDirection(pushDirection);
	                if (!tmpNode.isOccupied)
	                {
	                    t.GetComponent<GridNavMeshWrapper>().currentNode = tmpNode;
	                }
            }
        }

        Debug.Log(owner + " Executes " + name);

        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p != null) && (Vector3.Distance(owner.transform.position, p.transform.position) <= 1);
	}
}
