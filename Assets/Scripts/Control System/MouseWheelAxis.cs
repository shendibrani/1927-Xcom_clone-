using UnityEngine;
using System.Collections;

public class MouseWheelAxis : Axis
{
	void Update ()
	{
		_axisValue = Input.mouseScrollDelta.y;
	}
}

