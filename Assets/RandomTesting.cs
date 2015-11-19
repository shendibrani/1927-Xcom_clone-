using UnityEngine;
using System.Collections;

public class RandomTesting : MonoBehaviour {

	System.Random a,b,c;

	// Use this for initialization
	void Start () {
		a = new System.Random (666);
		b = new System.Random (666);
		c = new System.Random (666);

		Debug.Log ("a: " + a.Next () + " " + a.Next () + " " + a.Next () + " " + a.Next () + " " + a.Next () + " " + a.Next () + " ");
		Debug.Log ("a: " + b.NextDouble () + " " + b.NextDouble () + " " + b.NextDouble () + " " + b.NextDouble () + " " + b.NextDouble () + " " + b.Next () + " ");
		Debug.Log ("a: " + c.Next () + " " + c.NextDouble () + " " + c.Next () + " " + c.NextDouble () + " " + c.Next () + " " + c.Next () + " ");
	}
}
