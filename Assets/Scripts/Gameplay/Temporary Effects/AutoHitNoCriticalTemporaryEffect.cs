using UnityEngine;
using System.Collections;

public class AutoHitNoCriticalTemporaryEffect : PawnEffect {

    public AutoHitNoCriticalTemporaryEffect(Pawn pOwner)
        : base(pOwner, -1, EffectType.TEMPORARY)
    {
        hitMulti = 100d;
        critChanceMod = 0d;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { owner.EffectList.Remove(this); }
}
