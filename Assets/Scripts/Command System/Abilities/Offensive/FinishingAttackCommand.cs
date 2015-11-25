using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishingAttackCommand : PawnTargetingCommand
{

    Pawn target;
    int actionCost = 2;

    public override List<Pawn> validTargets
    {
        get { return base.validTargets.FindAll(x => x.GetComponent<Health>().health < x.GetComponent<Health>().maxHealth / 3); }
    }

    public FinishingAttackCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Execution Command";
        target = pTarget;
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        if (validTargets.Contains(target))
        {
            target.EffectList.Add(new FinishingAttackTemporaryEffect(target));

            return new AttackCommand(owner, target).Attack();
        }
        else
        {
            return false;
        }

    }
}
