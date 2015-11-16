using UnityEngine;
using System.Collections;

public class KeyboardToAxisLayer : Axis {

	[SerializeField] protected KeyCode plus;
	[SerializeField] protected KeyCode minus;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if(focus){
			_axisValue = 0;

			if(Input.GetKey(plus)){
				_axisValue = 1;
			} else if(Input.GetKey(minus)){
				_axisValue = -1;
			}
		}
	}
}
