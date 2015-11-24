using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Command
{
	public string name {get; protected set;}

	public Pawn owner {get; private set;}

	public Command(Pawn pOwner){
		owner = pOwner;
	}

	public abstract bool Execute ();

	public abstract bool Undo ();

	public override string ToString ()
	{
		return string.Format ("[Command: {0} executes {1}]", owner, name);
	}

    //check against specific action point cost and apply charge
    protected bool CheckCost(int cost)
    {
        if (owner.ActionPoints < cost)
        {
            Debug.Log("Not enough action points");
            return false;
        }
        else
        {
            owner.actionPointsSpent += cost;
            return true;
        }
    }
}

public abstract class NodeTargetingCommand : Command
{
	public NodeTargetingCommand (Pawn pOwner) : base (pOwner){}

	public abstract List<NodeBehaviour> validTargets { get; } 
}

public abstract class PawnTargetingCommand : Command
{
	public PawnTargetingCommand (Pawn pOwner) : base (pOwner){}
	
	public abstract List<Pawn> validTargets { get; } 
}

public abstract class TargetableTargetingCommand : Command
{
	public TargetableTargetingCommand (Pawn pOwner) : base (pOwner){}
	
	public abstract List<Targetable> validTargets { get; } 
}

