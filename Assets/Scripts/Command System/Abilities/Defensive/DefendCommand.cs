using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefendCommand : Command {

    int actionCost = 1;
	
    public DefendCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Defend Command";
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost) || !CheckTarget()) return false;

        foreach (Pawn p in validTargets)
        {
            p.EffectList.Add(Factory.GetEffect(Effects.DefendBuff, owner));
        }

        return true;
    }

	public override bool IsValidTarget(Targetable x){
		Pawn p = x as Pawn;
		return (p!= null) && (p.owner == owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.Weapon.range);
	}
}
