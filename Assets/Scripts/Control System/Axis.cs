using UnityEngine;
using System.Collections;

public abstract class Axis : MonoBehaviour {
	
	[SerializeField] protected float easingFactor = 0.6f;
	
	protected float _axisValue;

	protected bool focus { get; private set;}

	public float axisValue { get { return _axisValue; } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnApplicationFocus(bool focus){
		this.focus = focus;
	}
}
