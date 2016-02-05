using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Pawn))]
public class VisibleBasedOnLoS : MonoBehaviour
{
	private static List<VisibleBasedOnLoS> _all;
	
	public static List<VisibleBasedOnLoS> all {
		get{
			if(_all == null){
				_all = new List<VisibleBasedOnLoS>(FindObjectsOfType<VisibleBasedOnLoS>());
			}
			return _all;
		}
	}

	[SerializeField] bool debug;

	public List<Renderer> models;
	[SerializeField] ParticleSystem ripple;
	[SerializeField] TooltipBehaviour tooltip;

	public bool generatesLineOfSight;

	void Awake()
	{
		if (models == null) {
			models = new List<Renderer> ();
		}
	}
	void Start()
	{
		if (_all != null) {
			_all.Add (this);
		}

		GetComponent<Health> ().OnDeath.AddListener (StopGeneratingLoS);
		if(generatesLineOfSight){
			foreach (Renderer r in models) {
				r.enabled = true;
			}
			ripple.enableEmission = false;
		}
	}

	public void Visible ()
	{
		if (!generatesLineOfSight) {
			if (debug)
				Debug.Log ("Visible");
			tooltip.enabled = true;
			foreach (Renderer r in models) {
				r.enabled = true;
			}
			ripple.enableEmission = false;
		}
	}

	public void Hidden ()
	{
		if (!generatesLineOfSight) {
			if (debug)
				Debug.Log ("Hidden");
			tooltip.enabled = false;
			foreach (Renderer r in models) {
				r.enabled = false;
			}
			ripple.enableEmission = true;
		}
	}

	public void OutOfHearingRange ()
	{
		if (!generatesLineOfSight) {
			if (debug)
				Debug.Log ("OutOfHearingRange");
			tooltip.enabled = false;
			foreach (Renderer r in models) {
				r.enabled = false;
			}
			ripple.enableEmission = false;
		}
	}

	void StopGeneratingLoS(Pawn p)
	{
		generatesLineOfSight = false;
		LineOfSightManager.instance.StopGeneratingLoS (this);
	}

	#region Callbacks
	
	void OnDestroy(){
		if(_all != null)
			_all.Remove(this);
	}
	
	#endregion
}

