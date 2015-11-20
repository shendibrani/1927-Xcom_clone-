using UnityEngine;
using System.Collections;

public class ApplyChilled : WeaponEffect{
    public override void Execute(Pawn pOwner, Weapon pWeapon, Pawn pTarget)
    {
        if (RNG.NextDouble() < 0.2d)
        {
            Debug.Log(pTarget + " is chilled");
            PawnChilledDebuff effect = new PawnChilledDebuff(pTarget);
            pTarget.EffectList.Add(effect);
        }
    }
}
