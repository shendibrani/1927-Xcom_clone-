﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeCommand : NodeTargetingCommand {

    NodeBehaviour targetNode;
    int range = 5;
    int areaOfEffect = 2;
    int actionCost = 3;
    int damage = 3;

    public override List<NodeBehaviour> validTargets
    {
        get { return Pathfinder.NodesWithinSteps(owner.currentNode, range); }
    }

    public GrenadeCommand(Pawn pOwner, NodeBehaviour pTargetNode, int pRange)
        : base(pOwner)
    {
        name = "Grenade Command";
        targetNode = pTargetNode;
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        foreach (NodeBehaviour n in Pathfinder.NodesWithinSteps(targetNode, areaOfEffect))
        {
            if (n.currentObject != null) Hit(n.currentObject);
        }

        return true;
    }

    void Hit(Targetable pTarget)
    {
        if (pTarget is Pawn)
        {
            double hitChance = 1 - (1 - 0.5) * (Vector3.Distance(owner.transform.position, targetNode.transform.position) - 1) / (range - 1);

            if (RNG.NextDouble() < hitChance)
            {
                double accuracy = Vector3.Distance(targetNode.transform.position, (pTarget as Pawn).transform.position) / 200d;
                double tmpDamageMod = (1 - accuracy) * RNG.NextDouble() + accuracy;
                if (tmpDamageMod > 1d) tmpDamageMod = 1d;
                int tmpDamage = (int)System.Math.Round((double)damage * tmpDamageMod);
                if (tmpDamage < 1) tmpDamage = 1;
                Debug.Log(owner + " hit " + (pTarget as Pawn) + " and dealt " + tmpDamage + " damage.");
                (pTarget as Pawn).GetComponent<Health>().Damage(tmpDamage);
            }
        }
        else
        {

        }
    }

}
