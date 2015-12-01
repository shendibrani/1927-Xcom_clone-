using UnityEngine;
using System.Collections;

public class ApplyStunned : WeaponEffect
{
    public override void Execute(Pawn pOwner, Weapon pWeapon, Pawn pTarget)
    {
        if (RNG.NextDouble() < 0.2d)
        {
            Debug.Log(pTarget + " is stunned");
            PawnStunnedDebuff effect = new PawnStunnedDebuff(pTarget);
            pTarget.EffectList.Add(effect);
        }
    }
}
