using UnityEngine;
using System.Collections;

public class PawnAnimationManager : MonoBehaviour
{
	bool dirty;
	bool isAnimating;
	Animator animator;
	RuntimeAnimatorController animatorController;

	// Use this for initialization
	void Start ()
	{
		animatorController = GetComponentInChildren<RuntimeAnimatorController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (dirty) {
			dirty = false;
			return;
		}

		if (isAnimating) {
			GetComponentInChildren<Animator> ().SetBool ("Shooting", false);
		}

		if (isAnimating && GetComponentInChildren<Animator> ().GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
			TurnManager.instance.SetFree();
			isAnimating = false;
		}
	}

	public void SetShooting()
	{
		GetComponentInChildren<Animator> ().SetBool ("Shooting", true);
		isAnimating = true;
		dirty = true;
	}
}

