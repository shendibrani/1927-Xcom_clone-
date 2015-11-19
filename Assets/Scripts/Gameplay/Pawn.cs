using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridMovementBehaviour))]
[RequireComponent(typeof(Health))]
public class Pawn : MonoBehaviour
{
    public Player owner;
    //public Character character; //a reference to character, only used to initilise the pawn/update the character after level (could be stored in player for mission.) maybe use a passer

    public Weapon weapon = new Cryogun(); //either an instance of weapon or a reference (flywheel)

    int actionPointsPerTurn = 3;

    int movement;
    int actionPoints;
    int accuracy;

    public int Movement { 
        get { return (actionPoints * STEPSPERPOINT) + movementMod; }
        set { movement = value; }
    }
    
    public int ActionPoints 
    { 
        get { return actionPoints + actionPointsMod; } 
        private set { actionPoints = value; } 
    }
    
    public int Accuracy 
    { 
        get { return accuracy + accuracyMod; }
        private set { accuracy = value; } 
    }

    public const int STEPSPERPOINT = 3;

    public int movementMod;
    public int actionPointsMod;
    public int accuracyMod;

    List <PawnEffect> effectList;
    public List<PawnEffect> EffectList
    {
        get
        {
            if (effectList == null) effectList = new List<PawnEffect>();
            return effectList;
        }
    }

    public Command move;
    public Command attack;
    public List<Command> abilities;

    public int sightRange;

    void Start()
    {

    }

    public void Turn()
    {
        movementMod = 0;
        actionPointsMod = 0;
        accuracyMod = 0;
        for (int i = effectList.Count - 1; i < 0; i--)
        {
            effectList[i].OnTurn();
        }
    }

    public NodeBehaviour currentNode
    {
        get { return GetComponent<GridMovementBehaviour>().currentNode; }
    }

    public List<NodeBehaviour> reachableNodes
    {
        get { return Pathfinder.FindNodesWithinSteps(currentNode, movement); }
    }

    public override string ToString()
    {
        return name;
    }
    #region callbacks

    #endregion
}

