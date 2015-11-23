using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StormCommand : Command {

	List <object> targetList; //list of targetables? that pawn hits

	public StormCommand (Pawn pOwner, List<object> pTargetList): base (pOwner){
        name = "Storm Command";
        targetList = pTargetList;
	}

	public override bool Execute (){
        return true;
    }

	public override bool Undo (){
        return true;
    }
}
