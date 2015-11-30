﻿using UnityEngine;
using System.Collections;

public class LifestealCommand : Command {

    int actionCost = 4;

    public LifestealCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Execution Command";
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost) || !CheckTarget()) return false;

		Pawn tPawn = target.GetComponent<Pawn> ();

        tPawn.EffectList.Add(new LifestealTemporaryEffect(tPawn));

		AttackCommand.Attack(owner, tPawn);

		return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p!= null) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.Weapon.range);
	}
}
