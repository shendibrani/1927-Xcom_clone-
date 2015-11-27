using UnityEngine;
using System.Collections;

public class CounterTemporaryEffect : PawnEffect {

    public CounterTemporaryEffect(Pawn pOwner)
        : base(pOwner, 0, EffectType.POSITIVE)
    {
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { owner.GetComponent<Health>().Heal(value);
    pOther.GetComponent<Health>().Damage(value);
    owner.EffectList.Remove(this);
    }
    public override void OnRemove() {  }
}
