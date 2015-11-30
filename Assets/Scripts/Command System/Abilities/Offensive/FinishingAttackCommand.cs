using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishingAttackCommand : Command
{

   	int actionCost = 2;

    public FinishingAttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Execution Command";
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost) || !CheckTarget()) return false;

		Pawn tPawn = target.GetComponent<Pawn> ();

		tPawn.EffectList.Add(new FinishingAttackTemporaryEffect(tPawn));

		AttackCommand.Attack(owner, tPawn);

		return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p!= null) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.Weapon.range) && (p.GetComponent<Health>().health < p.GetComponent<Health>().maxHealth / 3);
	}
}
