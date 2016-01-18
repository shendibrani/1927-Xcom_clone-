using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NodeBehaviour))]
public class ReachNodeTrigger : Trigger
{
	[SerializeField] [Tooltip("Populate this if the pawn needs to belong to a specific player")] Pawn spectificPawn;
	[SerializeField] [Tooltip("Populate this id the node needs to be reached by a specific pawn")]Player specificPlayer;
	
	protected override bool Condition ()
	{
		if (spectificPawn != null) {
			return GetComponent<NodeBehaviour> ().currentObject == spectificPawn;
		} else if (specificPlayer != null) {
			return (GetComponent<NodeBehaviour> ().currentObject.GetComponent<Pawn>() != null) && (GetComponent<NodeBehaviour> ().currentObject.GetComponent<Pawn>().owner == specificPlayer);
		} else {
			return GetComponent<NodeBehaviour>().currentObject != null;
		}
	}

}

