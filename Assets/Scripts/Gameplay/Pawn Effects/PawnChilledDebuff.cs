using UnityEngine;
using System.Collections;

public class PawnChilledDebuff : PawnEffect {

    public PawnChilledDebuff(Pawn pOwner) : base(pOwner, 1, EffectType.NEGATIVE){
    }

    public override void OnTurn() {
        owner.actionPointsMod =- 1;
    }

    public override void OnRemove() { }
    public override void OnAttack() { }
    public override void OnDefense() { }
}
