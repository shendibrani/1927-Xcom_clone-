using UnityEngine;
using System.Collections;

public class AnimationTestscript : MonoBehaviour {

	bool shot = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
		anim.SetBool("Shooting",false);

		if (Input.GetKeyDown(KeyCode.A)) {
			anim.SetInteger("Weapon",0);
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			anim.SetInteger("Weapon",1);
		}

		if (Input.GetKeyDown(KeyCode.D)) {
			anim.SetBool("Ducking",true);
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			anim.SetBool("Ducking",false);
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			anim.SetBool("Shooting",true);
		}

		if (Input.GetKey(KeyCode.G)) {
			anim.SetBool("Moving",true);
		}
		if (Input.GetKey(KeyCode.H)) {
			anim.SetBool("Moving",false);
		}

	}
}
