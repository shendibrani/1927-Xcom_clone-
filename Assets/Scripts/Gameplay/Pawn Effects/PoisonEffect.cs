using UnityEngine;
using System.Collections;

public class PoisonEffect : PawnEffect
{
	int damage = 1;

	public PoisonEffect(Pawn pOwner)
		: base(pOwner, -1, EffectType.NEGATIVE)	{}
	
	public override void OnTurn() 
	{
		owner.GetComponent<Health> ().Damage (owner,damage);
	}
	public override void OnAttack() { }
	public override void OnDefense(Pawn pOther) { }
	public override void OnHit(Pawn pOther, int value) { }
	public override void OnRemove() { owner.EffectList.Remove(this); }
}

