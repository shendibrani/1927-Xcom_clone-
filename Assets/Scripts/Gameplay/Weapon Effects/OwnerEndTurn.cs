using UnityEngine;
using System.Collections;

public class OwnerEndTurn : WeaponEffect {

    public override void Execute(Pawn pOwner, Weapon pWeapon, Pawn pTarget)
    {
        EndTurnTemporaryEffect effect = new EndTurnTemporaryEffect(pOwner);
        pOwner.EffectList.Add(effect);
    }
}
