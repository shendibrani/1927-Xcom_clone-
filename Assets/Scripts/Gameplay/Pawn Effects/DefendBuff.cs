using UnityEngine;
using System.Collections;

public class DefendBuff : PawnEffect {

    public DefendBuff(Pawn pOwner)
        : base(pOwner, 1, EffectType.POSITIVE)
    {

    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { owner.GetComponent<Health>().Heal(value / 2); }
    public override void OnRemove() { }
}
