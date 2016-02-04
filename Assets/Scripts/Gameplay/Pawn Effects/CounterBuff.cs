using UnityEngine;
using System.Collections;

public class CounterBuff : PawnEffect
{
    public CounterBuff(Pawn pOwner)
        : base(pOwner, 0, EffectType.POSITIVE)
    {
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value) {
        Debug.Log(owner + " Counters");
        owner.GetComponent<Health>().Heal(value);
		pOther.GetComponent<Health>().Damage(owner,value);
    owner.EffectList.Remove(this);
    }
    public override void OnRemove() {  }
}
