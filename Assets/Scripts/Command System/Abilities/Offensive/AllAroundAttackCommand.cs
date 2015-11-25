using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AllAroundAttackCommand : PawnTargetingCommand
{
    Weapon weapon;
    int actionCost = 2;

    public AllAroundAttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Attack Command";
        weapon = owner.Weapon;
    }

    public override List<Pawn> validTargets { get { return base.validTargets.FindAll(x => Vector3.Distance(owner.transform.position, x.transform.position) < 1); } }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;
        if (validTargets.Count == 0)
        {
            Debug.Log("There are no valid targets");
            return false;
        }
        if (weapon.range == 1)
        {
            foreach (Pawn p in validTargets)
            {
                new AttackCommand(owner, p).Attack();
            }
            return true;
        }
        else
        {
            Debug.Log("Melee Weapon Not Equiped");
            return false;
        }
    }
}
