using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StormCommand : NodeTargetingCommand {

    int actionCost = 3;

	public StormCommand (Pawn pOwner): base (pOwner){
        name = "Storm Command";
        //targetList = pTargetList;
	}

	public override bool Execute (){
        return true;
    }
}
