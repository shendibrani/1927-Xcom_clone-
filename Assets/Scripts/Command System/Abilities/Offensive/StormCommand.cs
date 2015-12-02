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

        NodeBehaviour n = owner.currentNode;

        for (int i = 0; i < range; i++)
        {
            if (n.GetLinkInDirection(direction) != null)
            {
                n = n.GetLinkInDirection(direction);
                if (n.currentObject != null && n.currentObject.GetComponent<DestroyableProp>() != null) {n.currentObject.GetComponent<DestroyableProp>().DamageProp(); }
                foreach (LinkPositions d in LinkPositions.GetValues(typeof(LinkPositions)))
                {
                    if (n.GetLinkInDirection(d) != null)
                    {
                        Targetable c = n.GetLinkInDirection(d).currentObject;
                        if (c != null && c.GetComponent<DestroyableProp>() != null) { c.GetComponent<DestroyableProp>().DamageProp(); }
                    }
                }
            }
        }

        List<NodeBehaviour> path = Pathfinder.GetPath(owner.currentNode, n);

        owner.GetComponent<GridNavMeshWrapper>().SetPath(path);

        Debug.Log(owner + " Executes " + name);

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
                    if (n.currentObject != null && n.currentObject.GetComponent<Pawn>() != null) break;
                    tmpList.Add(n);
                }
            }
        }
        return tmpList;
    }
}
