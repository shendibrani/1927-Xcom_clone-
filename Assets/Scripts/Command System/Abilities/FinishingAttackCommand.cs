using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishingAttackCommand : PawnTargetingCommand
{

    Pawn target;
    Weapon weapon;
    int actionCost = 2;

    public override List<Pawn> validTargets
    {
        get { return base.validTargets.FindAll(x => x.GetComponent<Health>().health < x.GetComponent<Health>().maxHealth / 3); }
    }

    public FinishingAttackCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Execution";
        target = pTarget;
        weapon = owner.Weapon;
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        if (validTargets.Contains(target))
        {
            owner.EffectList.Add(new SureHitTemporaryEffect(owner));

            return new AttackCommand(owner, target).Attack();
        }
        else
        {
            return false;
        }

    }
}
