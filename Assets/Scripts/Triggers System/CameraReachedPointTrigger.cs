using UnityEngine;
using System.Collections;

public class CameraReachedPointTrigger : Trigger {

	bool _triggered = false;

	void OnTriggerEnter(Collider _other)
	{
		if (_other.tag == "CameraDolly") {
			_triggered = true;
		}
	}

	protected override bool Condition ()
	{
		return _triggered;
	}
}
