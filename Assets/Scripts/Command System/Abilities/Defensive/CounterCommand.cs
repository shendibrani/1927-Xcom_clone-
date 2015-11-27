using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CounterCommand : PawnTargetingCommand
{

	int actionCost = 2;

    public override List<Pawn> validTargets { get { return owner.sightList.FindAll(x => (Vector3.Distance(owner.transform.position, x.transform.position) < owner.Weapon.range) && (x.owner == owner.owner)); } }

    public CounterCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Counter Command";
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost)) return false;

        owner.EffectList.Add(Factory.GetEffect(Effects.CounterBuff,owner));

        return true;
    }
}
