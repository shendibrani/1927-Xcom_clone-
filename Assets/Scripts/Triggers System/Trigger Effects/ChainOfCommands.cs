using UnityEngine;
using System.Collections;

public class ChainOfCommands : MonoBehaviour
{
	//The three arrays need to have the same amount of objects in them, or the script will throw out of bounds exceptions and possibly crash everything.

	[SerializeField] Commands[] commands;
	[SerializeField] Pawn [] executors;
	[SerializeField] Targetable[] targets;

	// Update is called once per frame
	void Execute (Pawn p)
	{
		int counter = 0; 
		while (counter < commands.Length){
			if(!TurnManager.instance.busy){
				if(executors[counter] == null) {executors[counter] = p;}
				Command command = Factory.GetCommand(commands[counter], executors[counter]);
				command.freeExec = true;
				command.target = targets[counter];
				command.Execute();
				counter++;
			}
		}
	}
}

