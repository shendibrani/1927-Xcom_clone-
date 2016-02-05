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
            case Commands.Attack:
                return new AttackCommand(owner);
            case Commands.AbsorbShield:

                break;
            case Commands.AccuracyBuff:

                break;
            case Commands.ActionBoost:
                return new ActionBoostCommand(owner);
            case Commands.AimedAttack:
                return new AimedAttackCommand(owner);
            case Commands.AllAroundAttack:
                return new AllAroundAttackCommand(owner);
            case Commands.AllOrNothing:
                return new AllOrNothingCommand(owner);
            case Commands.AllyActionBoost:

                break;
            case Commands.Angst:

                break;
            case Commands.BattleShout:
                return new BattleShoutCommand(owner);
            case Commands.Counter:
                return new CounterCommand(owner);
            case Commands.DefenceBuff:

                break;
            case Commands.Defend:
                return new DefendCommand(owner);
            case Commands.Distraction:

                break;
            case Commands.FinishingAttack:
                return new FinishingAttackCommand(owner);
            case Commands.FirstAid:

                break;
            case Commands.Grenade:
                return new GrenadeCommand(owner);
            case Commands.GroupBuff:

                break;
            case Commands.HealArea:

                break;
            case Commands.Lifesteal:
                return new LifestealCommand(owner);
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
                return new PushCommand(owner);
            case Commands.Recover:
                return new RecoverCommand(owner);
            case Commands.ShieldAlly:

                break;
            case Commands.SmokeBomb:

                break;
            case Commands.Storm:
                return new StormCommand(owner);
            case Commands.Stun:

                break;
            case Commands.SureHit:
                return new SureHitCommand(owner);
            case Commands.Taunt:
                
                break;
            case Commands.TripleAttack:
                return new TripleAttackCommand(owner);
        }

        return null;
    }

    public static PawnEffect GetEffect(Effects type, Pawn owner)
    {
        switch (type)
        {
            case Effects.AccuracyBuff:
                return new AccuracyBuff(owner);
            case Effects.ActionPointBoost:
                return new ActionPointBoost(owner);
            case Effects.BlindDebuff:
                return new BlindDebuff(owner);
            case Effects.DefendBuff:
                return new DefendBuff(owner);
            case Effects.CounterBuff:
                return new CounterBuff(owner);
            case Effects.PawnChilledDebuff:
                return new PawnChilledDebuff(owner);
            case Effects.AimedMotivationTemporary:
                return new AimedMotivationTemporaryEffect(owner);
            case Effects.AllOrNothingTemporary:
                return new AllOrNothingTemporaryEffect(owner);
            case Effects.EndTurnTemporary:
                return new EndTurnTemporaryEffect(owner);
            case Effects.FinishingAttackTemporary:
                return new FinishingAttackTemporaryEffect(owner);
            case Effects.HalfAccuracyTemporary:
                return new HalfAccuracyTemporaryEffect(owner);
            case Effects.LifestealTemporary:
                return new LifestealTemporaryEffect(owner);
            case Effects.SureHitTemporary:
                return new SureHitTemporaryEffect(owner);
			case Effects.Poison:
			    return new PoisonEffect(owner);
        }

        return null;
    }

    public static WeaponEffect GetWeaponEffect(WeaponEffects type)
    {
        switch (type)
        {
            case WeaponEffects.WeaponChill:
                return new ApplyChilled();
            case WeaponEffects.WeaponStun:
                return new ApplyStunned();
            case WeaponEffects.WeaponEndTurn:
                return new OwnerEndTurn();
        }
        return null;
    }

    public static Character GetCharacter(CharacterClass cClass, int level = 1)
    {
        int nFirst = RNG.Next(0, Enum.GetNames(typeof(FirstName)).Length);
        int nLast = RNG.Next(0, Enum.GetNames(typeof(LastName)).Length);
        int nGender = RNG.Next(0, Enum.GetNames(typeof(Gender)).Length);
        return new Character(cClass, ((FirstName) nFirst).ToString() + ((LastName) nLast).ToString(), (Gender)nGender, level);
    }

    public static Character GetCharacter(int level = 1)
    {
        int nFirst = RNG.Next(0, Enum.GetNames(typeof(FirstName)).Length);
        int nLast = RNG.Next(0, Enum.GetNames(typeof(LastName)).Length);
        int nClass = RNG.Next(0, Enum.GetNames(typeof(CharacterClass)).Length);
        int nGender = RNG.Next(0, Enum.GetNames(typeof(Gender)).Length);
        return new Character((CharacterClass)nClass, ((FirstName)nFirst).ToString() + ((LastName)nLast).ToString(), (Gender)nGender, level);
    }

    public static Character GetEnemy(int level = 1)
    {
        int nClass = RNG.Next(0, Enum.GetNames(typeof(CharacterClass)).Length);
        int nWeapon = RNG.Next(0, Enum.GetNames(typeof(Weapons)).Length);
        int nGender = RNG.Next(0, Enum.GetNames(typeof(Gender)).Length);
        return new Character(RNG.Next(), "Enemy", (CharacterClass)nClass, (Weapons) nWeapon, level, (Gender)nGender);
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

    BlindDebuff,
	PawnChilledDebuff,
		
	AimedMotivationTemporary,
	AllOrNothingTemporary,
	EndTurnTemporary,
	FinishingAttackTemporary,
	HalfAccuracyTemporary,
	LifestealTemporary,
	SureHitTemporary,
	Poison
}

public enum FirstName
{
    Name,
    Name1,
    Nane2
}

public enum LastName
{
    Name,
    Name1,
    Name2
}
