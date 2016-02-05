using UnityEngine;
using System.Collections;

public class AIPlayer : Player
{	
	public override void Turn ()
	{
		Debug.Log (name + " is executing its turn.");
		foreach (Pawn p in pawns)
		{
			Debug.Log (p.name);
			p.Turn();
			StartCoroutine(WaitForFree());
		}
		TurnManager.instance.NextTurn();
	}

	IEnumerator WaitForFree(){
		while (TurnManager.instance.busy) {
			yield return null; // wait until next frame
		}
	}
}