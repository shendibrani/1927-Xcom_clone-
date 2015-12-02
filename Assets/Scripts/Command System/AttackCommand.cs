using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackCommand : Command
{
    public AttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Attack Command";
    }

    public override bool Execute()
    {
		if (!CheckCost (owner.Weapon.actionCost) || !CheckTarget ())
			return false;

		Pawn tPawn = target.GetComponent<Pawn> ();

		Attack (owner, tPawn);
		return true;
	}

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p!= null) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.Weapon.range);
	}

	public static void Attack (Pawn owner, Pawn target)
	{
		Weapon weapon = owner.Weapon;

		int effectiveRange = weapon.range;
		if (owner.transform.position.y - target.transform.position.y > 0) {
			effectiveRange += (int)((owner.transform.position.y - target.transform.position.y) / 2f);
		}
		double hitChance = 1 - (1 - 0.5) * (Vector3.Distance (owner.transform.position, target.transform.position) - 1) / (effectiveRange - 1);
		CoverState coverState = target.GetCoverState (owner);
		switch (coverState) {
		case CoverState.Half:
			owner.EffectList.Add (new HalfCoverDebuff (owner));
			break;
		case CoverState.Full:
			owner.EffectList.Add (new FullCoverDebuff (owner));
			break;
		}
        if (owner.hitMulti != 0) hitChance *= owner.hitMulti;
		Debug.Log (System.Math.Round (hitChance * 100) + "% chance to hit");
		if (RNG.NextDouble () < hitChance) {
			int damage = weapon.damage;
			double accuracy = (owner.Accuracy * owner.accuracyMulti + owner.accuracyMod) / 100d;
			if (1 - accuracy <= 0)
				accuracy = 1d;
			double tmpDamageMod = (1 - accuracy) * RNG.NextDouble () + accuracy;
			if (tmpDamageMod > 1d)
				tmpDamageMod = 1d;
			tmpDamageMod *= owner.damageMulti;
			if (RNG.NextDouble () < weapon.criticalChance + owner.critChanceMod) {
				damage = (int)System.Math.Round ((double)weapon.damage * 1.5d * tmpDamageMod);
				if (damage < 1)
					damage = 1;
				Debug.Log (owner + " critically hit " + target + " and dealt " + damage + " damage.");
				target.GetComponent<Health> ().Damage (damage);
			}
			else {
				damage = (int)System.Math.Round ((double)weapon.damage * tmpDamageMod);
				if (damage < 1)
					damage = 1;
				Debug.Log (owner + " hit " + target + " and dealt " + damage + " damage.");
				target.GetComponent<Health> ().Damage (damage);
			}
			if (weapon.weaponEffects != null) {
				for (int i = weapon.weaponEffects.Count - 1; i >= 0; i--) {
					//weapon.weaponEffects [i].Execute (owner, weapon, target);
				}
			}
			for (int i = target.EffectList.Count - 1; i >= 0; i--) {
				target.EffectList [i].OnHit (owner, damage);
			}
		}
		else {
			Debug.Log (owner + " missed " + target);
		}
		for (int i = owner.EffectList.Count - 1; i >= 0; i--) {
			owner.EffectList [i].OnRemove ();
		}
	}
}
