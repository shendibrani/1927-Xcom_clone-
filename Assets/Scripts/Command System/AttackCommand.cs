using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackCommand : Command
{
    Pawn target;
    Weapon weapon;

    public override System.Type targetType
    {
        get { return typeof(Pawn); }
    }

    public override IList validTargets { get { return owner.sightList.FindAll(x => Vector3.Distance(owner.transform.position, x.transform.position) < weapon.range); } }

    public AttackCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Attack Command";
        target = pTarget;
        weapon = owner.Weapon;
    }

    public override bool Execute()
    {

        if (!CheckCost(weapon.actionCost)) return false;

        return Attack();
    }

    public bool Attack()
    {
        if (owner.sightList.Contains(target))
        {
            List<Pawn> validTargets = owner.sightList.FindAll(x => Vector3.Distance(owner.transform.position, x.transform.position) <= weapon.range);

            if (validTargets.Contains(target))
            {

                if (target.GetComponent<Health>() == null)
                {
                    Debug.Log(target + " does not have health");
                    return false;
                }

                double hitChance = 1 - (1 - 0.5) * (Vector3.Distance(owner.transform.position, target.transform.position) - 1) / (weapon.range - 1);
                Debug.Log(System.Math.Round(hitChance * 100) + "% chance to hit");

                if (RNG.NextDouble() < hitChance)
                {
                    int damage = weapon.damage;
                    double accuracy = owner.Accuracy / 100d;
                    double tmpDamageMod = (1 - accuracy) * RNG.NextDouble() + accuracy;
                    if (tmpDamageMod > 1d) tmpDamageMod = 1d;

                    if (RNG.NextDouble() < weapon.criticalChance)
                    {
                        damage = (int)System.Math.Round((double)weapon.damage * 1.5d * accuracy);
                        if (damage < 1) damage = 1;
                        Debug.Log(owner + " critically hit " + target + " and dealt " + damage + " damage.");
                        target.GetComponent<Health>().Damage(damage);
                    }
                    else
                    {
                        damage = (int)System.Math.Round((double)weapon.damage * accuracy);
                        if (damage < 1) damage = 1;
                        Debug.Log(owner + " hit " + target + " and dealt " + weapon.damage + " damage.");
                        target.GetComponent<Health>().Damage(weapon.damage);
                    }
                    if (weapon.weaponEffects != null)
                    {
                        for (int i = weapon.weaponEffects.Count - 1; i >= 0; i--)
                        {
                            weapon.weaponEffects[i].Execute(owner, weapon, target);
                        }
                    }
                    for (int i = owner.EffectList.Count - 1; i >= 0; i--)
                    {
                        owner.EffectList[i].OnRemove();
                    }
                }
                else
                {
                    Debug.Log(owner + " missed " + target);
                }
                return true;
            }
        }
        return false;
    }

    public override bool Undo()
    {
        //target.GetComponent<Health>().Heal(weapon.damage);

        return true;
    }
}
