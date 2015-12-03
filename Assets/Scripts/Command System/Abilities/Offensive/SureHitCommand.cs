using UnityEngine;
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

        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

		Pawn tPawn = target.GetComponent<Pawn> ();

		tPawn.EffectList.Add(new SureHitTemporaryEffect(tPawn));

		AttackCommand.Attack(owner, target);

        Debug.Log(owner + " Executes " + name);

		return true;

    }

	public override bool IsValidTarget(Targetable t)
	{
        return AttackCommand.DefaultAttackIsValidTarget(t, owner);
	}
}
