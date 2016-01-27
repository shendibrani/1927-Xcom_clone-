using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NodeBehaviour))]
public class ReachNodeTrigger : Trigger
{
	[SerializeField] [Tooltip("Populate this if the pawn needs to belong to a specific player")] Pawn spectificPawn;
	[SerializeField] [Tooltip("Populate this id the node needs to be reached by a specific pawn")] Player specificPlayer;
	
	protected override bool Condition ()
	{
		if (spectificPawn != null) {
			if(GetComponent<NodeBehaviour> ().currentObject == spectificPawn){
				triggerer = spectificPawn;
				return true;
			}
		} else if (specificPlayer != null) {
			if((GetComponent<NodeBehaviour> ().currentObject.GetComponent<Pawn>() != null) && (GetComponent<NodeBehaviour> ().currentObject.GetComponent<Pawn>().owner == specificPlayer)){
				triggerer = GetComponent<NodeBehaviour> ().currentObject.GetComponent<Pawn>();
				return true;
			}
		} else {
			if(GetComponent<NodeBehaviour>().currentObject.GetComponents<Pawn>() != null){
				triggerer = GetComponent<NodeBehaviour> ().currentObject.GetComponent<Pawn>();
				return true;
			}
		}

		return false;
	}

}

