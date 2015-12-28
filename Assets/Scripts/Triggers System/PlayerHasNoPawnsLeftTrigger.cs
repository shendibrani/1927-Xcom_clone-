using UnityEngine;
using System.Collections;

public class PlayerHasNoPawnsLeftTrigger : Trigger
{
	[SerializeField] Player player;
	
	protected override bool Condition ()
	{
		return player.Pawns.Count == 0;
	}
	
}
