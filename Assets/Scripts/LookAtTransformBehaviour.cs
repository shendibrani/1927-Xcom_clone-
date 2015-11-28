using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LookAtTransformBehaviour : MonoBehaviour {
	
	[SerializeField] Transform target;
	
	// Update is called once per frame
	void Update () 
	{
		if(target != null){
			transform.LookAt(target);
		}
	}
}
