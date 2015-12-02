using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionBoostCommand : Command
{
    int actionCost = 0;

    public ActionBoostCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Action Boost Command";
        targetsAllValidTargets = true;
    }

    public override bool Execute()
    {
        if (!CheckCost (actionCost))
			return false;

        owner.EffectList.Add(Factory.GetEffect(Effects.ActionPointBoost, owner));
        owner.EffectList.Add(Factory.GetEffect(Effects.EndTurnTemporary, owner));

        Debug.Log(owner + " Executes " + name);

        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return p == owner;
	}
}
