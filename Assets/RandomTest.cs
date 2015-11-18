using UnityEngine;
using System.Collections;

public class RandomTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		RNG.SetSeed (666);
		RNG.Next ();
		RNG.Next ();
		RNG.Next ();
		RNG.Next ();
		RNG.Next ();
		Debug.Log (RNG.Next ());

		RNG.Reset (666, RNG.RNGCount-1);
		Debug.Log (RNG.Next ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
