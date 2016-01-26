using UnityEngine;
using System.Collections;

public class ChainOfCommands : MonoBehaviour
{
	//The three arrays need to have the same amount of objects in them, or the script will throw out of bounds exceptions and possibly crash everything.

	[SerializeField] Commands[] commands;
	[SerializeField] Pawn [] executors;
	[SerializeField] Targetable[] targets;

	// Update is called once per frame
	void Execute ()
	{
		for (int counter = 0; counter < commands.Length; counter++){
			Command command = Factory.GetCommand(commands[counter], executors[counter]);
			command.target = targets[counter];
			command.Execute();
		}
	}
}

