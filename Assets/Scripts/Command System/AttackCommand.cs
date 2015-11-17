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
		weapon = owner.weapon;
    }

	public override bool Execute ()
	{
		if(target.GetComponent<Health>() == null){
			return false;
		}

        if (RNG.NextFloat() > 0.5f)
        {
			Debug.Log(owner + " hit " + target + "and dealt " + weapon.damage + " damage.");
			target.GetComponent<Health>().Damage(weapon.damage);
        }
        else
        {
            Debug.Log(owner + " missed " + target);
        }

		return true;
	}

	public override bool Undo ()
	{
		target.GetComponent<Health>().Heal(weapon.damage);

		return true;
	}
}
