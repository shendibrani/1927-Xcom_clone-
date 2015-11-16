using UnityEngine;
using System.Collections;

public class VerticalEdgePanAxis : Axis {

	[SerializeField] float tolerance = 25;

	// Update is called once per frame
	void Update () 
	{
		_axisValue = 0;
		if(focus){
			if(Input.mousePosition.y <= tolerance){
				_axisValue = -1;
			} else if(Input.mousePosition.y >= Screen.height - tolerance){
				_axisValue = 1;
			}
		}
	}
}