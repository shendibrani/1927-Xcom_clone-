using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SureHitCommand : Command {

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

        owner.EffectList.Add(new SureHitTemporaryEffect(owner));

        return new AttackCommand(owner, target).Execute();

    }

    public override bool Undo()
    {
        //target.GetComponent<Health>().Heal(weapon.damage);

        return true;
    }
}
