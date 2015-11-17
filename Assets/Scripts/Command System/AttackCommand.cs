using UnityEngine;
using System.Collections;

public class AttackCommand : Command {

	Pawn target;
    Weapon weapon;

	public AttackCommand(Pawn pOwner, Weapon pWeapon, Pawn pTarget) : base(pOwner)
	{
		name = "Attack Command";
		target = pTarget;
        weapon = pWeapon;
	}

	public override bool Execute ()
	{
		
		//if (path == null || path.Count == 0) {
			//also send UI feedback at some point
		//	return false;
		//}
		//owner.GetComponent<GridMovementBehaviour> ().SetPath (path);
		//also send UI feedback at some point
		return true;
	}

	public override bool Undo ()
	{
		//owner.GetComponent<GridMovementBehaviour> ().currentNode = originalPosition;
		//owner.GetComponent<GridMovementBehaviour> ().position = originalPosition.offsetPosition;
		//also send UI feedback at some point
		return true;
	}
}
