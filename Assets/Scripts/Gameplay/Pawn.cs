using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridMovementBehaviour))]
[RequireComponent(typeof(Health))]
public class Pawn : MonoBehaviour
{
	public int movement { get { return actionPoints * STEPSPERPOINT; } }
    public int actionPoints { get; private set; }
    [SerializeField] int actionPointsPerTurn = 3;
    public const int STEPSPERPOINT = 3;

    public Command move;
	public Command attack;
	public List<Command> abilities; 

	public int sightRange;

	public NodeBehaviour currentNode { 
		get { return GetComponent<GridMovementBehaviour> ().currentNode;}
	}

    public List<NodeBehaviour> reachableNodes
    {
        get { return Pathfinder.FindNodesWithinSteps(currentNode, movement); }
    }

	public Player owner;

	public Weapon weapon;

    public override string ToString ()
	{
		return name;
	}

	#region callbacks

	#endregion
}

