using UnityEngine;
using System.Collections;

public class PawnChilledDebuff : PawnEffect {

    public PawnChilledDebuff(Pawn pOwner) : base(pOwner, 1, EffectType.NEGATIVE){
        actionPointMod = -3;
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { }
    public override void OnRemove() { }
}
