using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StormCommand : Command
{

    int actionCost = 3;
    int range = 4;

    public StormCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Storm Command";
    }

    public override bool Execute()
    {
        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

        LinkPositions direction = owner.currentNode.GetRelativePosition(target.GetComponent<NodeBehaviour>());

        return true;
    }

    public override bool IsValidTarget(Targetable t)
    {
        NodeBehaviour n = t.GetComponent<NodeBehaviour>();
        return (n != null) && (CheckLines().Contains(n));
    }

    public List<NodeBehaviour> CheckLines()
    {
        List<NodeBehaviour> tmpList = new List<NodeBehaviour>();
        foreach (LinkPositions direction in LinkPositions.GetValues(typeof(LinkPositions)))
        {
            NodeBehaviour n = owner.currentNode;
            for (int i = 0; i < range; i++)
            {
                if (n.GetLinkInDirection(direction) != null)
                {
                    n = n.GetLinkInDirection(direction);
                    tmpList.Add(n);
                }
            }
        }
        return tmpList;
    }
}
