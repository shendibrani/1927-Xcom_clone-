﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SureHitCommand : Command {
	
    int actionCost = 2;

    public SureHitCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Sure Hit Command";
    }

    public override bool Execute()
    {

		if (!CheckCost(actionCost) || !CheckTarget()) return false;

		Pawn tPawn = target as Pawn;

		tPawn.EffectList.Add(new SureHitTemporaryEffect(tPawn));

		AttackCommand.Attack(owner, tPawn);

		return true;

    }

	public override bool IsValidTarget(Targetable x)
	{
		Pawn p = x as Pawn;
		return (p!= null) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.Weapon.range);
	}
}
