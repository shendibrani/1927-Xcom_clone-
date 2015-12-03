using UnityEngine;
using System.Collections;

public class AimedAttackCommand : Command {

	int actionCost = 4;

    public AimedAttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Aimed Attack Command";
    }

    public override bool Execute()
    {
        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

		Pawn tPawn = target.GetComponent<Pawn>();

        owner.EffectList.Add(Factory.GetEffect(Effects.AimedMotivationTemporary, owner));

		AttackCommand.Attack(owner, target);

        Debug.Log(owner + " Executes " + name);

		return true;
    }

	public override bool IsValidTarget(Targetable t){
        return AttackCommand.DefaultAttackIsValidTarget(t, owner);
	}
}
