using UnityEngine;
using System.Collections;

public class RecoverCommand : Command {

	int actionCost = 2;

    public RecoverCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Recover Command";
        targetsAllValidTargets = true;
    }

    public override bool Execute()
    {
        if (!CheckCost(actionCost)) return false;

        owner.GetComponent<Health>().Heal(owner.GetComponent<Health>().maxHealth / 10);

        Debug.Log(owner + " Executes " + name);

        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return p == owner;
	}
}
