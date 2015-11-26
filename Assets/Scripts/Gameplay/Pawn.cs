using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridNavMeshWrapper))]
[RequireComponent(typeof(Health))]
public class Pawn : MonoBehaviour, Targetable
{
    public Player owner;

    //unique id for the pawn to determine who is doing what
    int pawnID;

    Character character;
    //public Character character; //a reference to character, only used to initilise the pawn/update the character after level (could be stored in player for mission.) maybe use a passer

    public Weapon Weapon
    {
        get;
        private set;
    }//either an instance of weapon or a reference (flywheel)

    //Weapon weapon;

    int actionPointsPerTurn = 3;
	public int MaxActionPointsPerTurn {	get { return actionPointsPerTurn; } }

    int actionPoints = 3;
    int movement;
    public int accuracy
    {
        get;
        private set;
    }

	#region Properties
    public int ActionPoints
    {
        get { return actionPointsPerTurn + actionPointsMod - actionPointsSpent; }
        private set { actionPoints = value; }
    }

    public int Movement
    {
        get { return (ActionPoints * STEPSPERPOINT); }
        set { movement = value; }
    }

    public int Accuracy
    {
        get { return accuracy + accuracyMod; }
        private set { accuracy = value; }
    }

    public const int STEPSPERPOINT = 3;

    [HideInInspector]
    public int actionPointsSpent;

    public int actionPointsMod
    {
        get
        {
            int tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.actionPointMod;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.actionPointMod;
            }
            return tmp;
        }
    }
    public int accuracyMod
    {
        get
        {
            int tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.accuracyMod;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.accuracyMod;
            }
            return tmp;
        }
    }
    public double accuracyMulti
    {
        get
        {
            double tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.accuracyMulti;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.accuracyMulti;
            }
            return tmp;
        }
    }
    public double hitMod
    {
        get
        {
            double tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.hitMod;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.hitMod;
            }
            return tmp;
        }
    }
    public double hitMulti
    {
        get
        {
            double tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.hitMulti;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.hitMulti;
            }
            return tmp;
        }
    }
    public double damageMod
    {
        get
        {
            double tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.damageMod;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.damageMod;
            }
            return tmp;
        }
    }
    public double damageMulti
    {
        get
        {
            double tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.damageMulti;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.damageMulti;
            }
            return tmp;
        }
    }
    public double critChanceMod
    {
        get
        {
            double tmp = 0;
			if(currentNode.tileEffect != null){
				tmp += currentNode.tileEffect.critChanceMod;
			}
			foreach (PawnEffect e in EffectList)
            {
                tmp += e.critChanceMod;
            }
            return tmp;
        }
    }

    List<PawnEffect> effectList;
    public List<PawnEffect> EffectList
    {
        get
        {
            if (effectList == null) effectList = new List<PawnEffect>();
            return effectList;
        }
    }

    public static int sightRange = 10;

    public List<Pawn> sightList
    {
        get { return LineOfSightManager.GetSightList(this); }
    }

	#endregion

    public Command move;
    public Command attack;
    public List<Skill> skillList;

    void Start()
    {
    }

    public void Initalise(Character pCharacter)
    {
        character = pCharacter;
        Weapon = pCharacter.assignedWeapon;
        actionPoints = pCharacter.actionPoints;
        actionPointsPerTurn = pCharacter.actionPoints;
        accuracy = pCharacter.accuracy;
        skillList = new List<Skill>(pCharacter.skillList);
    }

    public void Turn()
    {
        actionPointsSpent = 0;

		if(currentNode.tileEffect != null){
			currentNode.tileEffect.Turn();
		}
		for (int i = EffectList.Count - 1; i >= 0; i--)
        {
            EffectList[i].Turn();
        }
    }

    public NodeBehaviour currentNode
    {
        get { return GetComponent<GridNavMeshWrapper>().currentNode; }
    }

    public List<NodeBehaviour> reachableNodes
    {
        get { return Pathfinder.NodesWithinSteps(currentNode, Movement); }
    }

    public override string ToString()
    {
        return name;
    }

    public CoverState GetCoverState(Pawn other)
    {
        Vector3 direction = other.currentNode.position - currentNode.position;
        direction.Normalize();

        if (Physics.Raycast(transform.position + (Vector3.up * 1.5f), direction, 1f))
        {
            return CoverState.Full;
        }
        if (Physics.Raycast(transform.position + (Vector3.up * 0.5f), direction, 1f))
        {
            return CoverState.Half;
        }

        return CoverState.None;
    }

	public void OnTargeted(Pawn targeter)
	{

	}

    #region Callbacks

    #endregion
}

public enum CoverState
{
    None, Half, Full
}