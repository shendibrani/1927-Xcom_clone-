using UnityEngine;
using System.Collections;

public class ActionPointBoost : PawnEffect {

    public ActionPointBoost(Pawn pOwner)
        : base(pOwner, 1, EffectType.POSITIVE)
    {
        actionPointMod += 3;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { }
}
