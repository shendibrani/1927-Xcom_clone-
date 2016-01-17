using UnityEngine;
using System.Collections;

public class RandomLoad : MonoBehaviour {

	[ExecuteInEditMode]

	//Randomly Spawns one of the Objects in the list, then deletes itself

	public GameObject[] _Objects;
	
	public GameObject Generate () 
	{
		int i = Random.Range (0, _Objects.Length);

		GameObject _object = (GameObject) Instantiate (_Objects [i],transform.position,transform.rotation);

		return _object;
	}
}
