using UnityEngine;
using System.Collections;

public class HalfAccuracyTemporaryEffect : PawnEffect {

    public HalfAccuracyTemporaryEffect(Pawn pOwner)
        : base(pOwner, -1, EffectType.TEMPORARY)
    {
        accuracyMulti = 0.5d;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { owner.EffectList.Remove(this); }
}
