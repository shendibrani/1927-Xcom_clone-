using UnityEngine;
using System.Collections;

public class BlindDebuff : PawnEffect
{

    public BlindDebuff(Pawn pOwner)
        : base(pOwner, 2, EffectType.POSITIVE)
    {
        accuracyMulti += 0.5;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { }
}
