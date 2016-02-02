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
		animator = GetComponentInChildren<Animator> ();
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
			animator.SetBool ("Shooting", false);
		}

		if (isAnimating && animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
			TurnManager.instance.SetFree();
			isAnimating = false;
		}
	}

	public void SetShooting()
	{
		animator.SetBool ("Shooting", true);
		isAnimating = true;
		dirty = true;
	}
}

