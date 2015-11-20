using UnityEngine;
using System.Collections;

public abstract class PawnEffect {

    protected Pawn owner { get; private set; }
    int lifetime;
    int lifetimeCounter;
    public EffectType effectType;

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
    public abstract void OnDefense();
    public abstract void OnRemove();
}

public enum EffectType
{
    NEGATIVE,
    POSITIVE
}
 