using UnityEngine;
using System.Collections;

public abstract class WeaponEffect {
    public abstract void Execute(Pawn pOwner, Weapon pWeapon, Pawn pTarget);
}
