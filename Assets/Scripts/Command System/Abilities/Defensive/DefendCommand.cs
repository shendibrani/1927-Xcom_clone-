using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefendCommand : Command {

    int actionCost = 1;
    int defendRange = 2;
	
    public DefendCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Defend Command";
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        foreach (Targetable t in validTargets)
        {
            t.GetComponent<Pawn>().EffectList.Add(Factory.GetEffect(Effects.DefendBuff, owner));
        }

        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p!= null) && (p.owner == owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < defendRange);
	}
}
