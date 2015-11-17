using UnityEngine;
using System.Collections;

public class AttackCommand : Command {

	Pawn target;
    Weapon weapon;

    public AttackCommand(Pawn pOwner, Pawn pTarget)
        : base(pOwner)
    {
        name = "Attack Command";
        target = pTarget;
    }

	public override bool Execute ()
	{
        if (Random.value > 0.5f)
        {
            Debug.Log(owner + " hit " + target);
        }
        else
        {
            Debug.Log(owner + " missed " + target);
        }
		return true;
	}

	public override bool Undo ()
	{
		return true;
	}
}
