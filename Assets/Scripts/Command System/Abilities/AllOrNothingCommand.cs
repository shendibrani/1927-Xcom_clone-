using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllOrNothingCommand : Command
{

    Pawn target;
    Weapon weapon;
    int actionCost = 2;

    public AllOrNothingCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Vital Strike Command";
        target = pTarget;
        weapon = owner.Weapon;
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;
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
                Debug.Log(System.Math.Round(hitChance * 50) + "% chance to hit");

                int damage = weapon.damage;
                double accuracy = (owner.Accuracy / 100d) + RNG.NextDouble();
                if (accuracy > 1d) accuracy = 1d;
                damage = (int)System.Math.Round((double)weapon.damage * 1.5d * accuracy);

                if (damage < 1) damage = 1;
                Debug.Log(owner + " hit " + target + " and dealt " + damage + " damage.");
                target.GetComponent<Health>().Damage(damage);

                if (weapon.weaponEffects != null)
                {
                    for (int i = weapon.weaponEffects.Count - 1; i >= 0; i--)
                    {
                        weapon.weaponEffects[i].Execute(owner, weapon, target);
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
        return true;
    }
}
