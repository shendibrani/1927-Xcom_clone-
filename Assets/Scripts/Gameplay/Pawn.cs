using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridNavMeshWrapper))]
[RequireComponent(typeof(Health))]
public class Pawn : MonoBehaviour, Targetable
{
    public Player owner;
    //public Character character; //a reference to character, only used to initilise the pawn/update the character after level (could be stored in player for mission.) maybe use a passer

    public Weapon Weapon
    {
        get;
        private set;
    }//either an instance of weapon or a reference (flywheel)

    //Weapon weapon;

    int actionPointsPerTurn = 3;
    int movement;
    int actionPoints = 3;
    public int accuracy
    {
        get;
        private set;
    }

    public int ActionPoints
    {
        get { return actionPoints + actionPointsMod - actionPointsSpent; }
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

   

    public Command move;
    public Command attack;
    public List<Command> abilities;

    void Start()
    {
        Weapon = new AssaultRifle();
    }

    public void Turn()
    {
        actionPointsSpent = 0;

        for (int i = effectList.Count - 1; i >= 0; i--)
        {
            effectList[i].Turn();
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
        GetComponent<PawnHighlightingManager>().SetState(PawnHighlightStates.Targetable);
    }

    #region Callbacks

    #endregion
}

public enum CoverState
{
    None, Half, Full
}