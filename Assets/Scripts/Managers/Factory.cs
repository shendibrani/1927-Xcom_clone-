using System;
using UnityEngine;

public static class Factory
{
    public static Command GetCommand(Commands type, Pawn owner)
    {
        switch (type)
        {
            case Commands.Move:
                return new MoveCommand(owner);
                break;
            case Commands.Attack:
                return new AttackCommand(owner);
                break;
            case Commands.AbsorbShield:

                break;
            case Commands.AccuracyBuff:

                break;
            case Commands.ActionBoost:
                return new ActionBoostCommand(owner);
                break;
            case Commands.AimedAttack:
                return new AimedAttackCommand(owner);
                break;
            case Commands.AllAroundAttack:
                return new AllAroundAttackCommand(owner);
                break;
            case Commands.AllOrNothing:
                return new AllOrNothingCommand(owner);
                break;
            case Commands.AllyActionBoost:

                break;
            case Commands.Angst:

                break;
            case Commands.BattleShout:
                return new BattleShoutCommand(owner);
                break;
            case Commands.Counter:

                break;
            case Commands.DefenceBuff:

                break;
            case Commands.Defend:
                return new DefendCommand(owner);
                break;
            case Commands.Distraction:

                break;
            case Commands.FinishingAttack:
                return new FinishingAttackCommand(owner);
                break;
            case Commands.FirstAid:

                break;
            case Commands.Grenade:
                return new GrenadeCommand(owner);
                break;
            case Commands.GroupBuff:

                break;
            case Commands.HealArea:

                break;
            case Commands.Lifesteal:
                return new LifestealCommand(owner);
                break;
            case Commands.Medicine:

                break;
            case Commands.Revive:

                break;
            case Commands.Motivate:

                break;
            case Commands.NapalmBarricade:

                break;
            case Commands.NuclearBlood:

                break;
            case Commands.Push:

                break;
            case Commands.Recover:

                break;
            case Commands.ShieldAlly:

                break;
            case Commands.SmokeBomb:

                break;
            case Commands.Storm:
                return new StormCommand(owner);
                break;
            case Commands.Stun:

                break;
            case Commands.SureHit:
                return new SureHitCommand(owner);
                break;
            case Commands.Taunt:

                break;
            case Commands.TripleAttack:
                return new TripleAttackCommand(owner);
                break;
        }

        return null;
    }

    public static PawnEffect GetEffect(Effects type, Pawn owner)
    {
        switch (type)
        {
            case Effects.AccuracyBuff:
                return new AccuracyBuff(owner);
                break;
            case Effects.ActionPointBoost:
                return new ActionPointBoost(owner);
                break;
            case Effects.DefendBuff:
                return new DefendBuff(owner);
                break;
            case Effects.CounterBuff:
                //return new DefendBuff(owner);
                break;
            case Effects.PawnChilledDebuff:
                return new PawnChilledDebuff(owner);
                break;
            case Effects.AimedMotivationTemporary:
                return new AimedMotivationTemporaryEffect(owner);
                break;
            case Effects.AllOrNothingTemporary:
                return new AllOrNothingTemporaryEffect(owner);
                break;
            case Effects.EndTurnTemporary:
                return new EndTurnTemporaryEffect(owner);
                break;
            case Effects.FinishingAttackTemporary:
                return new FinishingAttackTemporaryEffect(owner);
                break;
            case Effects.HalfAccuracyTemporary:
                return new HalfAccuracyTemporaryEffect(owner);
                break;
            case Effects.LifestealTemporary:
                return new LifestealTemporaryEffect(owner);
                break;
            case Effects.SureHitTemporary:
                return new SureHitTemporaryEffect(owner);
                break;
        }

        return null;
    }
}

public enum Commands 
{
	Move,
	Attack,
		
	ActionBoost,
	AimedAttack,
	AllAroundAttack, 
	AllOrNothing, 
	BattleShout,
	FinishingAttack,
	Grenade, 
	Lifesteal, 
	Storm,
	SureHit,
	TripleAttack,

	Defend,
	Counter,
	Push,
	SmokeBomb,
	Recover,
	ShieldAlly,
	Stun,
	Taunt,
	NapalmBarricade,
	NuclearBlood,
	Angst,
		
	FirstAid,
	Medicine,
    Revive,
	AccuracyBuff,
	DefenceBuff,
	AllyActionBoost,
	HealArea,
	AbsorbShield,
	Motivate,
	Distraction,
	GroupBuff
}

public enum Effects 
{
	AccuracyBuff,
	ActionPointBoost,
	DefendBuff,
    CounterBuff,

	PawnChilledDebuff,
		
	AimedMotivationTemporary,
	AllOrNothingTemporary,
	EndTurnTemporary,
	FinishingAttackTemporary,
	HalfAccuracyTemporary,
	LifestealTemporary,
	SureHitTemporary
}
