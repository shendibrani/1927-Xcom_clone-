using UnityEngine;
using System.Collections;

public abstract class PawnEffect {

    protected Pawn owner { get; private set; }
    int lifetime;
    int lifetimeCounter;

    public PawnEffect(Pawn pOwner, int pLifetime){
        owner = pOwner;
        lifetime = pLifetime;
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
}
 