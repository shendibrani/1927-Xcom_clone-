using UnityEngine;
using System.Collections;

public class AccuracyBuff : PawnEffect {

    public AccuracyBuff(Pawn pOwner)
        : base(pOwner, 2, EffectType.POSITIVE)
    {
        if (owner.accuracy < 100) accuracyMod += (100 - owner.accuracy) / 2;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { }
}
