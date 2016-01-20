using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomLoad : MonoBehaviour {

	[ExecuteInEditMode]

	//Randomly Spawns one of the Objects in the list, then deletes itself

	public GameObject[] _Objects;
	public int[] _chance;
	public int[] _MaximumHeight;
	public bool _RandomRotation = false;

	List<GameObject> _items = new List<GameObject>();


	public GameObject Generate () 
	{
		for (int i = 0; i < _Objects.Length; i++)
		{
			int _length = _chance[i];
			for (int j = 0; j < _length; j++)
			{
				if (transform.position.y <= _MaximumHeight[i]) {
					_items.Add(_Objects[i]);
				}
			}
		}

		int _i = Random.Range (0, _items.Count);

		GameObject _object = (GameObject) Instantiate (_items [_i],transform.position,transform.rotation);

		return _object;
	}
}
