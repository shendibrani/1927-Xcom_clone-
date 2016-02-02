using UnityEngine;
using System.Collections;

public class PawnAnimationManager : MonoBehaviour
{
	bool skipFrame;
	bool isAnimating;
	Animator _animator;
	Animator animator {
		get {
			if (_animator == null) {
				_animator = GetComponentInChildren<Animator> ();
			}
			return _animator;
		}
	}

	RuntimeAnimatorController animatorController;

	// Use this for initialization
	void Start ()
	{
		animatorController = GetComponentInChildren<RuntimeAnimatorController> ();
		GetComponent<Health> ().OnDeath.AddListener (SetDead);
		GetComponent<Health> ().OnDamage += SetDamaged;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (skipFrame) {
			skipFrame = false;
			return;
		}

		if (isAnimating) {
			GetComponentInChildren<Animator> ().SetBool ("Shooting", false);
			GetComponentInChildren<Animator> ().SetBool ("Damaged", false);
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
		skipFrame = true;
	}

	public void SetDead(Pawn p)
	{
		GetComponentInChildren<Animator> ().SetBool ("Dead", true);
	}

	public void SetDamaged(Pawn p, int Damage)
	{
		GetComponentInChildren<Animator> ().SetBool ("Damaged", true);
		skipFrame = true;
	}
}

