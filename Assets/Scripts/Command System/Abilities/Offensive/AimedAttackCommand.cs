using UnityEngine;
using System.Collections;

public class AimedAttackCommand : PawnTargetingCommand {

	Pawn target;
    int actionCost = 4;

    public AimedAttackCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Sure Hit Command";
        target = pTarget;
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        target.EffectList.Add(new SureHitTemporaryEffect(target));

        return new AttackCommand(owner, target).Attack();

    }
}
