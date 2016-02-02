using UnityEngine;
using System.Collections;

public class PawnAnimationManager : MonoBehaviour
{
	bool dirty;
	Animator animator;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (dirty) {
			dirty = false;
			return;
		}

		animator.SetBool ("Shooting", false);
	}

	public void SetShooting()
	{
		animator.SetBool ("Shooting", true);
		dirty = true;
	}
}

