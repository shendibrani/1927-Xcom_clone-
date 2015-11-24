using UnityEngine;
using System.Collections;

public abstract class PawnEffect {

    protected Pawn owner { get; private set; }
    int lifetime;
    int lifetimeCounter;
    public EffectType effectType { get; private set; }

    public int actionPointMod { get; protected set; }
    public int accuracyMod { get; protected set; }
    public int accuracyMulti { get; protected set; }
    public double hitMod { get; protected set; }
    public double hitMulti { get; protected set; }
    public double damageMod { get; protected set; }
    public double damageMulti { get; protected set; }
    public double critChanceMod { get; protected set; }

    public PawnEffect(Pawn pOwner, int pLifetime, EffectType pType){
        owner = pOwner;
        lifetime = pLifetime;
        effectType = pType;
    }

    public void Turn()
    {
        if (lifetime >= lifetimeCounter) { owner.EffectList.Remove(this); }
        OnTurn();
        lifetimeCounter++;
    }

    public abstract void OnTurn();
    public abstract void OnAttack();
    //called at the start of the attack on the target, modifies the attack from external defenses
    public abstract void OnDefense(Pawn pOther);
    //actively called on the target if an attack hits, responds to the attack
    public abstract void OnHit(Pawn pOther, int value);
    //called at the end of the attack and removes the effect from list
    public abstract void OnRemove();
}

public enum EffectType
{
    NEGATIVE,
    POSITIVE,
    TEMPORARY
}
 