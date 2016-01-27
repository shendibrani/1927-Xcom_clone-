using UnityEngine;
using System.Collections;

public class NumberOfUnitsKilled : Trigger
{
	[SerializeField] [Tooltip("Leave this as null if the kills can be from any player")] Player specificPlayer;
	[SerializeField] int numberOfKills;
	[SerializeField] [Tooltip("Check to make the trigger go off every time the set number of kills is made.")] bool recurring;
	int counter = 0;

	void Start (){
		if (specificPlayer != null) {

			foreach(Pawn p in specificPlayer.Pawns){
				p.GetComponent<Health>().OnDeath.AddListener(AddDeath);
			}

		} else { 

			foreach(Pawn p in FindObjectsOfType<Pawn>()){
				p.GetComponent<Health>().OnDeath.AddListener(AddDeath);
			}

		}

		if (recurring) {
			fulfilled.AddListener(Reset);
		}
	}

	void AddDeath(Pawn p) {
		if(p!= null){
			counter++;
		}
	}

	void Reset(){
		counter = 0;
	}

	protected override bool Condition ()
	{
		return numberOfKills == counter;
	}
	
}

