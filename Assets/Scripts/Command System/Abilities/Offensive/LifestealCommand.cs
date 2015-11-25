using UnityEngine;
using System.Collections;

public class LifestealCommand : PawnTargetingCommand {

	Pawn target;
    int actionCost = 4;

    public LifestealCommand(Pawn pOwner, Pawn pTarget)
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
            target.EffectList.Add(new LifestealTemporaryEffect(target));

            return new AttackCommand(owner, target).Attack();
        }
        else
        {
            return false;
        }

    }
}
