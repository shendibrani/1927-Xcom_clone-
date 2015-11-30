using UnityEngine;
using System.Collections;

public class TripleAttackCommand : Command
{
    int actionCost;

    public TripleAttackCommand(Pawn pOwner)
        : base(pOwner)
    {
        name = "Rage Attack Command";
        actionCost = owner.Weapon.actionCost * 2;
    }

    public override bool Execute()
    {

        if (!CheckCost(actionCost) || !CheckTarget()) return false;

		Pawn tPawn = target as Pawn;

        for (int c = 0; c < 3; c++)
        {
			tPawn.EffectList.Add(new HalfAccuracyTemporaryEffect(tPawn));
			AttackCommand.Attack(owner, tPawn);
        }
        return true;
    }

	public override bool IsValidTarget(Targetable t)
	{
		Pawn p = t.GetComponent<Pawn>();
		return (p!= null) && (p.owner != owner.owner) && (Vector3.Distance(owner.transform.position, p.transform.position) < owner.Weapon.range);
	}
}
