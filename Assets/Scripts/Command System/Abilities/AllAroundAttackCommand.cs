using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AllAroundAttackCommand : Command
{

    Weapon weapon;
    int actionCost = 2;

    public AllAroundAttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Attack Command";
        weapon = owner.Weapon;
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;
        //checks all targets in sight for one square
        //List<Pawn> validTargets = owner.sightList.FindAll(x => Vector3.Distance(owner.transform.position, x.transform.position) <= 1);
        //validTargets = validTargets.FindAll(x => x.owner != owner.owner);
        //if (validTargets.Count == 0)
        //{
        //    Debug.Log("There are no valid targets");
        //    return false;
        //}
        if (weapon.range == 1)
        {
            //foreach (Pawn p in validTargets)
            //{
            //    new AttackCommand(owner, p).Attack();
            //}
            return true;
        }
        else
        {
            Debug.Log("Melee Weapon Not Equiped");
            return false;
        }
    }

    public override bool Undo()
    {
        //target.GetComponent<Health>().Heal(weapon.damage);

        return true;
    }
}
