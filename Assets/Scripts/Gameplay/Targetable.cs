using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetable : MonoBehaviour
{
	private static List<Targetable> _all;
	
	public static List<Targetable> all {
		get{
			if(_all == null){
				_all = new List<Targetable>(FindObjectsOfType<Targetable>());
			}
			return _all;
		}
	}

	void Start(){
		if (_all != null) {
			_all.Add (this);
		}
	}

	public delegate void TargetingEvent(Pawn targeter);

	public TargetingEvent IsValidTarget;
	public TargetingEvent IsTargeted;
	public TargetingEvent NotTarget;

	#region Callbacks
	
	void OnDestroy(){
		if(_all != null)
			_all.Remove(this);
	}
	
	#endregion
}

