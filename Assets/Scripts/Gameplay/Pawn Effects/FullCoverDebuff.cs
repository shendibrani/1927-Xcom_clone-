using UnityEngine;
using System.Collections;

public class FullCoverDebuff : PawnEffect {

    public FullCoverDebuff(Pawn pOwner)
        : base(pOwner, -1, EffectType.TEMPORARY)
    {
        hitMulti *= 0.5d;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { owner.EffectList.Remove(this); }
}
