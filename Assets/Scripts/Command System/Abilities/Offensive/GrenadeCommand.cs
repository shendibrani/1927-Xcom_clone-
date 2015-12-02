using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadeCommand : Command {
	
    int range = 5;
    int areaOfEffect = 2;
    int actionCost = 3;
    int damage = 3;

    public GrenadeCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Grenade Command";
    }

    public override bool Execute()
    {
        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

		NodeBehaviour targetNode = target.GetComponent<NodeBehaviour> ();

        foreach (NodeBehaviour n in Pathfinder.NodesWithinSteps(targetNode, areaOfEffect))
        {
            if (n.currentObject != null) Hit(targetNode, n.currentObject);
        }

        Debug.Log(owner + " Executes " + name);

        return true;
    }

	void Hit(NodeBehaviour targetNode, Targetable pTarget)
    {
		if (pTarget.GetComponent<Pawn>() != null)
        {
            double hitChance = 1 - (1 - 0.5) * (Vector3.Distance(owner.transform.position, targetNode.transform.position) - 1) / (range - 1);

            if (RNG.NextDouble() < hitChance)
            {
				double accuracy = Vector3.Distance(targetNode.transform.position, pTarget.GetComponent<Pawn>().transform.position) / 200d;
                double tmpDamageMod = (1 - accuracy) * RNG.NextDouble() + accuracy;
                if (tmpDamageMod > 1d) tmpDamageMod = 1d;
                int tmpDamage = (int)System.Math.Round((double)damage * tmpDamageMod);
                if (tmpDamage < 1) tmpDamage = 1;
				Debug.Log(owner + " hit " + (pTarget.GetComponent<Pawn>()) + " and dealt " + tmpDamage + " damage.");
                pTarget.GetComponent<Health>().Damage(tmpDamage);
            }
        }
        if (pTarget.GetComponent<DestroyableProp>() != null)
        {

        }
    }

	public override bool IsValidTarget (Targetable t)
	{
		NodeBehaviour n = t.GetComponent<NodeBehaviour>();
		return (n != null) && (Pathfinder.NodesWithinSteps (owner.currentNode, range).Contains (n));
	}
}
