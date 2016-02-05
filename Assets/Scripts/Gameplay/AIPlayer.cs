using UnityEngine;
using System.Collections;

public class AIPlayer : Player
{
	public override void Turn ()
	{
		base.Turn ();
		TurnManager.instance.NextTurn();
	}
}

