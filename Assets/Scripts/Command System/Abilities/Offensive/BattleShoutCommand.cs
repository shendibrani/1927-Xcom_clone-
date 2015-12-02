using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleShoutCommand : Command
{	
    int actionCost = 4;

    public BattleShoutCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Battle Cry Command";
        targetsAllValidTargets = true;
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost))
            return false;
 
        foreach (Targetable t in validTargets)
        {
			t.GetComponent<Pawn>().EffectList.Add(Factory.GetEffect(Effects.AccuracyBuff, t.GetComponent<Pawn>()));
        }

        Debug.Log(owner + " Executes " + name);

        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p != null) && (owner.owner == p.owner); 
	}
}
