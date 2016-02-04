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
        if (!CheckTarget() || !CheckCost(owner.weapon.actionCost))
            return false;

        Debug.Log(owner + " Executes " + name);

        Pawn tPawn = target.GetComponent<Pawn>();
		owner.GetComponent<PawnAnimationManager> ().SetShooting (target);
        Attack(owner, target);
        return true;
    }

    public override bool IsValidTarget(Targetable t)
    {
        Pawn p = t.GetComponent<Pawn>();
        DestroyableProp d = t.GetComponent<DestroyableProp>();
        if (p != null)
            return (p != null) && (!p.isDead) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.weapon.range);
        else if (d != null)
            return (d != null) && (!p.isDead) && (Vector3.Distance(owner.transform.position, d.transform.position) < owner.weapon.range);
        else
            return false;
    }

    public static bool DefaultAttackIsValidTarget(Targetable t, Pawn owner)
    {
        Pawn p = t.GetComponent<Pawn>();
        DestroyableProp d = t.GetComponent<DestroyableProp>();
        if (p != null)
            return (p != null) && (!p.isDead) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.weapon.range);
        else if (d != null)
            return (d != null) && (!p.isDead) && (Vector3.Distance(owner.transform.position, d.transform.position) < owner.weapon.range);
        else
            return false;
    }

    public static void Attack(Pawn owner, Targetable target)
    {
        if (target.GetComponent<DestroyableProp>() != null)
        {
            Weapon weapon = owner.weapon;

            int effectiveRange = weapon.range;
            if (owner.transform.position.y - target.transform.position.y > 0)
            {
                effectiveRange += (int)((owner.transform.position.y - target.transform.position.y) / 2f);
            }
            double hitChance = 1 - (1 - 0.5) * (Vector3.Distance(owner.transform.position, target.transform.position) - 1) / (effectiveRange - 1);
            if (owner.hitMulti != 0) hitChance *= owner.hitMulti;
            Debug.Log(System.Math.Round(hitChance * 100) + "% chance to hit");
            if (RNG.NextDouble() < hitChance)
            {
                Debug.Log(owner + " hit " + target);
                target.GetComponent<Health>().Damage(1);
            }
            else
            {
                Debug.Log(owner + " missed " + target);
            }
            for (int i = owner.EffectList.Count - 1; i >= 0; i--)
            {
                owner.EffectList[i].OnRemove();
            }
        }
        else if (target.GetComponent<Pawn>() != null)
        {
            Weapon weapon = owner.weapon;

            int effectiveRange = weapon.range;
            if (owner.transform.position.y - target.transform.position.y > 0)
            {
                effectiveRange += (int)((owner.transform.position.y - target.transform.position.y) / 2f);
            }
            double hitChance = 1 - (1 - 0.5) * (Vector3.Distance(owner.transform.position, target.transform.position) - 1) / (effectiveRange - 1);
            CoverState coverState = target.GetComponent<Pawn>().GetCoverState(owner);
            switch (coverState)
            {
                case CoverState.Half:
                    owner.EffectList.Add(new HalfCoverDebuff(owner));
                    break;
                case CoverState.Full:
                    owner.EffectList.Add(new FullCoverDebuff(owner));
                    break;
            }
            if (owner.hitMulti != 0) hitChance *= owner.hitMulti;
            Debug.Log(System.Math.Round(hitChance * 100) + "% chance to hit");
            if (RNG.NextDouble() < hitChance)
            {
                int damage = weapon.damage;
                double accuracy = owner.Accuracy;
                if (owner.accuracyMulti != 0) accuracy *= owner.accuracyMulti;
                accuracy = (accuracy + owner.accuracyMod) / 100d;
                if (1 - accuracy <= 0)
                    accuracy = 1d;
                double tmpDamageMod = (1 - accuracy) * RNG.NextDouble() + accuracy;
                if (tmpDamageMod > 1d)
                    tmpDamageMod = 1d;
                if (owner.damageMulti != 0) tmpDamageMod *= owner.damageMulti;
                if (RNG.NextDouble() < weapon.criticalChance + owner.critChanceMod)
                {
                    damage = (int)System.Math.Round((double)weapon.damage * 1.5d * tmpDamageMod);
                    if (damage < 1)
                        damage = 1;
                    Debug.Log(owner + " critically hit " + target + " and dealt " + damage + " damage.");
                    target.GetComponent<Health>().Damage(damage);
                }
                else
                {
                    damage = (int)System.Math.Round((double)weapon.damage * tmpDamageMod);
                    if (damage < 1)
                        damage = 1;
                    Debug.Log(owner + " hit " + target + " and dealt " + damage + " damage.");
                    target.GetComponent<Health>().Damage(damage);
                }
                if (weapon.weaponEffects != null)
                {
                    for (int i = weapon.weaponEffects.Count - 1; i >= 0; i--)
                    {
                        Factory.GetWeaponEffect(weapon.weaponEffects[i]).Execute(owner, weapon, target.GetComponent<Pawn>());
                    }
                }
                for (int i = target.GetComponent<Pawn>().EffectList.Count - 1; i >= 0; i--)
                {
                    target.GetComponent<Pawn>().EffectList[i].OnHit(owner, damage);
                }
            }
            else
            {
                Debug.Log(owner + " missed " + target);
            }
            for (int i = owner.EffectList.Count - 1; i >= 0; i--)
            {
                owner.EffectList[i].OnRemove();
            }
        }
    }
}
