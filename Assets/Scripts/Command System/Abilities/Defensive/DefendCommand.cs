using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefendCommand : PawnTargetingCommand {

    int actionCost = 1;

    public override List<Pawn> validTargets { get { return owner.sightList.FindAll(x => (Vector3.Distance(owner.transform.position, x.transform.position) < owner.Weapon.range) && (x.owner == owner.owner)); } }

    public DefendCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Defend Command";
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        foreach (Pawn p in validTargets)
        {
            p.EffectList.Add(Factory.GetEffect(Effects.DefendBuff, owner));
        }

        return true;
    }
}
