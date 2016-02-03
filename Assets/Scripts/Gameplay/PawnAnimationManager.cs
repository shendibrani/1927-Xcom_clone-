using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Pawn))]
public class PawnAnimationManager : MonoBehaviour
{
	[SerializeField] bool debug;

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

	// Use this for initialization
	void Start ()
	{
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

		animator.SetBool ("Shooting", false);
		animator.SetBool ("Damaged", false);

		if (isAnimating && animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
			TurnManager.instance.SetFree();
			isAnimating = false;
		}
	}

	public void SetShooting(Targetable p)
	{
		if (debug) Debug.Log ("Set Shooting Called");
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
		if (debug) Debug.Log ("Set Dead Called");
		animator.SetBool ("Dead", true);
		
	}

	public void SetDamaged(Pawn p, int Damage)
	{
		if (debug) Debug.Log ("Set Damaged Called");
		animator.SetBool ("Damaged", true);
		skipFrame = true;
	}
}

