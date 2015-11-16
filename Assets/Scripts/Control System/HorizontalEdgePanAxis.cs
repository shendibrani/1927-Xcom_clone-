using UnityEngine;
using System.Collections;

public class HorizontalEdgePanAxis : Axis {
	
	[SerializeField] float tolerance = 25;
	
	// Update is called once per frame
	void Update () 
	{
		_axisValue = 0;
		if(focus){
			if(Input.mousePosition.x <= tolerance){
				_axisValue = -1;
			} else if(Input.mousePosition.x >= Screen.width - tolerance){
				_axisValue = 1;
			}
		}
	}
}