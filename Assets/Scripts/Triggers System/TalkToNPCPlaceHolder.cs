using UnityEngine;
using System.Collections;

public class TalkToNPCPlaceHolder : Trigger {

	protected override bool Condition ()
	{
		if (Input.GetKeyUp (KeyCode.Space))
		{
			return true;
		}
		return false;
	}
}
