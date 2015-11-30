using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CounterCommand : Command
{
	int actionCost = 2;

    public CounterCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Counter Command";
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        owner.EffectList.Add(Factory.GetEffect(Effects.CounterBuff,owner));

        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return p == owner;
	}
}
