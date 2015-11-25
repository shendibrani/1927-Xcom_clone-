using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleShoutCommand : PawnTargetingCommand
{

    int actionCost = 4;

    public override List<Pawn> validTargets
    {
        get
        {
            return owner.owner.Pawns;
        }
    }

    public BattleShoutCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Battle Cry Command";
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        foreach (Pawn p in validTargets)
        {
            owner.EffectList.Add(new AccuracyBuff(owner));
        }
        return true;
    }
}
