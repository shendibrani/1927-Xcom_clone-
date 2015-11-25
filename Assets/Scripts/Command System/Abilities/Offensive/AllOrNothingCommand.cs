using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllOrNothingCommand : PawnTargetingCommand
{

    Pawn target;
    int actionCost = 2;

    public AllOrNothingCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Vital Strike Command";
        target = pTarget;
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        owner.EffectList.Add(new AllOrNothingTemporaryEffect(owner));

        return new AttackCommand(owner, target).Attack();
    }
}
