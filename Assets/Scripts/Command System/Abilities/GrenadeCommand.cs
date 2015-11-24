using UnityEngine;
using System.Collections;

public class GrenadeCommand : Command {

    NodeBehaviour targetNode;
    int range = 5;
    int actionCost = 3;

    public GrenadeCommand(Pawn pOwner, NodeBehaviour pTargetNode, int pRange)
        : base(pOwner)
    {
        name = "Grenade Command";
        targetNode = pTargetNode;
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        return true;
    }

    public override bool Undo()
    {
        return true;
    }
}
