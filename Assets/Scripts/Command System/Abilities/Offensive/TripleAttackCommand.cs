using UnityEngine;
using System.Collections;

public class TripleAttackCommand : Command
{
    int actionCost;

    public TripleAttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Rage Attack Command";
        actionCost = owner.weapon.actionCost * 2;
    }

    public override bool Execute()
    {

        if (!CheckTarget() || !CheckCost(actionCost))
            return false;

		Pawn tPawn = target.GetComponent<Pawn> ();

        for (int c = 0; c < 3; c++)
        {
			owner.EffectList.Add(new HalfAccuracyTemporaryEffect(owner));
			AttackCommand.Attack(owner, target);
        }

        Debug.Log(owner + " Executes " + name);

        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
        return AttackCommand.DefaultAttackIsValidTarget(t, owner);
	}
}
