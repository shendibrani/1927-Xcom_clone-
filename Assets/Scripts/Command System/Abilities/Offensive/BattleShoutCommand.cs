using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleShoutCommand : Command
{	
    int actionCost = 4;

    public BattleShoutCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Battle Cry Command";
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        foreach (Pawn p in validTargets)
        {
            p.EffectList.Add(new AccuracyBuff(p));
        }
        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p != null) && (owner.owner == p.owner); 
	}
}
