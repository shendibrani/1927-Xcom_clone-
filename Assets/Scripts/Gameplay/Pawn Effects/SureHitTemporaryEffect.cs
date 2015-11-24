using UnityEngine;
using System.Collections;

public class SureHitTemporaryEffect : PawnEffect {

    public SureHitTemporaryEffect(Pawn pOwner)
        : base(pOwner, -1, EffectType.TEMPORARY)
    {
        hitMulti = 2;
        damageMulti = 0.5;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { owner.EffectList.Remove(this); }
}
