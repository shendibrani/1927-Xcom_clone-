using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllOrNothingCommand : Command
{

    Pawn target;
    Weapon weapon;
    int actionCost = 2;

    public AllOrNothingCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Vital Strike Command";
        target = pTarget;
        weapon = owner.Weapon;
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        owner.EffectList.Add(new AllOrNothingTemporaryEffect(owner));

        return new AttackCommand(owner, target).Attack();
    }

    public override bool Undo()
    {
        return true;
    }
}
