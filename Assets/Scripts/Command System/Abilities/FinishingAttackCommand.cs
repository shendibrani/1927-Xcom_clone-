using UnityEngine;
using System.Collections;

public class FinishingAttackCommand : Command {

	 Pawn target;
    Weapon weapon;
    int actionCost = 2;

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

        owner.EffectList.Add(new SureHitTemporaryEffect(owner));

        return new AttackCommand(owner, target).Attack();

    }

    public override bool Undo()
    {
        //target.GetComponent<Health>().Heal(weapon.damage);

        return true;
    }
}
