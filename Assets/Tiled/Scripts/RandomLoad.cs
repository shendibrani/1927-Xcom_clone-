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

	public bool _FuncProp = false;
	public GameObject _FunctionalityObject;

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

		GameObject _object;

		if (_FuncProp)
		{
			_object = (GameObject) Instantiate (_FunctionalityObject ,transform.position,transform.rotation);
			GameObject _ModelObject = (GameObject) Instantiate (_items [_i],transform.position,transform.rotation);

			if (_object.transform.childCount > 0) {
				_ModelObject.transform.parent = _object.transform.GetChild(0).transform;
			}
			else {
				_ModelObject.transform.parent = _object.transform;
			}
		}
		else
		{
			_object = (GameObject) Instantiate (_items [_i],transform.position,transform.rotation);
		}


		return _object;
	}
}
