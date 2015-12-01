using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Pawn))]
public class VisibleBasedOnLoS : MonoBehaviour
{
	[SerializeField] bool debug;

	[SerializeField] Renderer[] model;
	[SerializeField] ParticleSystem ripple;

	public bool generatesLineOfSight;

	public void Visible ()
	{
		if (!generatesLineOfSight) {
			if (debug)
				Debug.Log ("Visible");
			foreach (Renderer r in model) {
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
			foreach (Renderer r in model) {
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
			foreach (Renderer r in model) {
				r.enabled = false;
			}
			ripple.enableEmission = false;
		}
	}
}

