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
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)||!CheckTarget()) return false;
        owner.EffectList.Add(new ActionPointBoost(owner));
        owner.EffectList.Add(new EndTurnTemporaryEffect(owner));
        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn x = t as Pawn;
		return x == owner;
	}
}
