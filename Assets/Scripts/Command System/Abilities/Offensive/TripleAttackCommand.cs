using UnityEngine;
using System.Collections;

public class TripleAttackCommand : PawnTargetingCommand
{
    Pawn target;
    int actionCost;

    public TripleAttackCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Rage Attack Command";
        target = pTarget;
        actionCost = owner.Weapon.actionCost * 2;
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        for (int c = 0; c < 3; c++)
        {
            target.EffectList.Add(new HalfAccuracyTemporaryEffect(target));
            if (!new AttackCommand(owner, target).Attack()) return false;
        }
        return true;
    }
}
