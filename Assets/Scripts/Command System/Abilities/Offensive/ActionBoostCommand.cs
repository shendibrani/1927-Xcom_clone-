using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionBoostCommand : PawnTargetingCommand
{

    int actionCost = 0;

    public override List<Pawn> validTargets
    {
        get { return base.validTargets.FindAll(x => x == owner); }
    }

    public ActionBoostCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Action Boost Command";
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;
        owner.EffectList.Add(new ActionPointBoost(owner));
        owner.EffectList.Add(new EndTurnTemporaryEffect(owner));
        return true;
    }
}
