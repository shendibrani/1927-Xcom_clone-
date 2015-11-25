using UnityEngine;
using System.Collections;

public class LifestealTemporaryEffect : PawnEffect {

    public LifestealTemporaryEffect(Pawn pOwner)
        : base(pOwner, -1, EffectType.TEMPORARY)
    {
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) { pOther.GetComponent<Health>().Heal(value);
    owner.EffectList.Remove(this);
    }
    public override void OnRemove() {  }
}
