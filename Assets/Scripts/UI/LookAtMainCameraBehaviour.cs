using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAtMainCameraBehaviour : MonoBehaviour {
	
	Transform target;

	void Start ()
	{
		target = Camera.main.transform;
	}

	// Update is called once per frame
	void Update () 
	{
		if(target != null){
			transform.LookAt(target);
		}
	}
}
