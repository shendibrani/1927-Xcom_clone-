using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Pawn))]
public class VisibleBasedOnLoS : MonoBehaviour
{
	[SerializeField] bool debug;

	public List<Renderer> models;
	[SerializeField] ParticleSystem ripple;

	public bool generatesLineOfSight;

	void Awake()
	{
		if (models == null) {
			models = new List<Renderer> ();
		}
	}
	void Start()
	{
		GetComponent<Health> ().OnDeath.AddListener (StopGeneratingLoS);
	}

	public void Visible ()
	{
		if (!generatesLineOfSight) {
			if (debug)
				Debug.Log ("Visible");
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
}

