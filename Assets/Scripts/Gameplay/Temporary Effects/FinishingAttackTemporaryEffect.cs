using UnityEngine;
using System.Collections;

public class FinishingAttackTemporaryEffect : PawnEffect
{

    public FinishingAttackTemporaryEffect(Pawn pOwner)
        : base(pOwner, -1, EffectType.TEMPORARY)
    {
    }

    public override void OnTurn() { }
    public override void OnAttack() { }
    public override void OnDefense(Pawn pOther) { }
    public override void OnHit(Pawn pOther, int value)
    {
		owner.GetComponent<Health>().Damage(owner,owner.GetComponent<Health>().health);
        owner.EffectList.Remove(this);
    }
    public override void OnRemove() { owner.EffectList.Remove(this); }
}
