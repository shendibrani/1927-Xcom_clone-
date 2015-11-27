using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushCommand : PawnTargetingCommand
{

    Weapon weapon;
    Pawn target;
    int actionCost = 2;
    int distance = 3;

    public PushCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Push Command";
        target = pTarget;
        weapon = owner.Weapon;
    }

    public override List<Pawn> validTargets { get { return base.validTargets.FindAll(x => Vector3.Distance(owner.transform.position, x.transform.position) < 1); } }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        if (weapon.range == 1)
        {
            new AttackCommand(owner, target).Attack();
        }
        LinkPositions pushDirection;
        pushDirection = owner.currentNode.GetRelativePosition(target.currentNode);
        for (int i = 0; i < distance; i++)
        {
            NodeBehaviour tmpNode = target.currentNode.GetLinkInDirection(pushDirection);
            if (!tmpNode.isOccupied)
            {
                target.GetComponent<GridNavMeshWrapper>().currentNode = tmpNode;
            }
        }
        return true;
    }
}
