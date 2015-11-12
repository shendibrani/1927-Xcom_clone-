using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilityFunctions {


	/// <summary>
	/// Gets the closest object to the position passed as an argument with a component of type T.
	/// </summary>
	/// <returns>The component of type T of the closest gameobject to have one.</returns>
	/// <param name="position">The position.</param>
	/// <typeparam name="T">The type of component the algorythm is looking for.</typeparam>
	public static T GetClosest<T> (Vector3 position) where T : MonoBehaviour {

		List<T> tObjects = new List<T>(GameObject.FindObjectsOfType<T>());

		tObjects.Sort((x,y) => Vector3.Distance(position, x.transform.position).CompareTo(Vector3.Distance(y.transform.position, position)));

		return tObjects[0];
	}	
}
