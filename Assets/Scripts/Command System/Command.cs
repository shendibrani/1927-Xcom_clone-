using UnityEngine;
using System.Collections;

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
}

