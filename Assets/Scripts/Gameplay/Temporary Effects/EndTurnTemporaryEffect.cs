using UnityEngine;
using System.Collections;

public class EndTurnTemporaryEffect : PawnEffect {

    public EndTurnTemporaryEffect(Pawn pOwner)
        : base(pOwner, 0, EffectType.TEMPORARY)
    {
        actionPointMod -= 99;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { }
}
