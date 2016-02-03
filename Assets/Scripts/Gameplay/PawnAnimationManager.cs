using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Pawn))]
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
			animator.SetBool ("Shooting", false);
			animator.SetBool ("Damaged", false);
		}

		if (isAnimating && animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
			TurnManager.instance.SetFree();
			isAnimating = false;
		}
	}

	public void SetShooting(Targetable p)
	{
		animator.SetBool ("Shooting", true);
		SoundManager.instance.PlaySound (SoundEffects.ASSAULT,GetComponent<AudioSource>());

		animator.gameObject.transform.LookAt (new Vector3(p.transform.position.x, animator.transform.position.y, p.transform.position.z));
		if (p.GetComponent<Pawn> () != null) {
			animator.SetBool ("HighCoverInWay", GetComponent<Pawn> ().GetCoverState (p.GetComponent<Pawn>()) == CoverState.Full);
		}

		isAnimating = true;
		skipFrame = true;
	}

	public void SetDead(Pawn p)
	{
		animator.SetBool ("Dead", true);
		
	}

	public void SetDamaged(Pawn p, int Damage)
	{
		animator.SetBool ("Damaged", true);
		skipFrame = true;
	}
}

