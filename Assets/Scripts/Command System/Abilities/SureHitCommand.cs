﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SureHitCommand : PawnTargetingCommand {

    Pawn target;
    Weapon weapon;
    int actionCost = 2;

    public SureHitCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Sure Hit Command";
        target = pTarget;
        weapon = owner.Weapon;
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        target.EffectList.Add(new SureHitTemporaryEffect(target));

        return new AttackCommand(owner, target).Attack();

    }
}
