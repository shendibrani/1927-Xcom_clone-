using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllOrNothingCommand : Command
{

    int actionCost = 2;

    public AllOrNothingCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Vital Strike Command";
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost) || !CheckTarget()) return false;

        owner.EffectList.Add(new AllOrNothingTemporaryEffect(owner));
		Pawn tPawn = target as Pawn;

		AttackCommand.Attack(owner, tPawn);

		return true;
    }

	public override bool IsValidTarget(Targetable x){
		Pawn p = x as Pawn;
		return (p!= null) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.Weapon.range);
	}
}
