using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Command
{
	public string name {get; protected set;}

	public Pawn owner {get; private set;}

	public Targetable target;

	public List<Targetable> validTargets { get {return owner.targetsList.FindAll(x => x != null && IsValidTarget(x)); } }

	public bool targetsAllValidTargets { get; protected set; }

	public bool freeExec;

	//TODO: move the cost to class level if applicable.

	public Command(Pawn pOwner){
		owner = pOwner;
	}

	public abstract bool Execute ();

	/// <summary>
	/// Determines whether t is a valid target for this command. 
	/// Used to prune the owner's targetlist to calculate the valid targets.
	/// If the command has multiple conditions, start with the lighter ones to speed the process.
	/// Sort operations from lightest to heaviest when determining the condition.
	/// </summary>
	/// <returns><c>true</c> if t is a valid target for the command; otherwise, <c>false</c>.</returns>
	/// <param name="t"> The targetable of the gameobject that is being analised.</param>
	public abstract bool IsValidTarget (Targetable t);

	public override string ToString ()
	{
		return string.Format ("[Command: {0} executes {1}]", owner, name);
	}
	
	//check against specific action point cost and apply charge
	protected bool CheckCost(int cost)
	{
		if(freeExec){return true;}

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

	protected bool CheckTarget()
	{
		return(target != null && validTargets.Contains (target));
	}
}
