using UnityEngine;
using System.Collections;

public class AIPlayer : Player
{
	public override void Turn ()
	{
		base.Turn ();
		StartCoroutine(WaitForFree());
		TurnManager.instance.NextTurn();
	}

	IEnumerator WaitForFree(){
		while (TurnManager.instance.busy) {
			yield return null; // wait until next frame
		}
	}
}