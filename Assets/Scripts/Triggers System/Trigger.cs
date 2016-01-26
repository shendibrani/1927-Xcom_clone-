using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class Trigger : MonoBehaviour {


	[SerializeField] protected UnityEvent fulfilled;

	[SerializeField] [Tooltip("Tick this if this trigger needs to fire only once and deactivate")] protected bool oneOff;

	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Condition ()) {
			fulfilled.Invoke();
			if(oneOff){
				this.enabled = false;
			}
		}
	}

	protected abstract bool Condition();
}
