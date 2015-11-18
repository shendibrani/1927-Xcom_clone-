using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public static class UtilityFunctions {


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

    public static List<T> FindAllObjectsWithinRange<T>(Vector3 position, float distance) where T : MonoBehaviour
    {
        List<T> objects = new List<T>();

        foreach (T thing in GameObject.FindObjectsOfType<T>())
        {
            if (Vector3.Distance(position, thing.transform.position) < distance)
            {
                objects.Add(thing);
            }
        }

        return objects;
    }

    public static GameObject FindGameObjectThroughNetID(NetworkInstanceId id)
    {
        NetworkIdentity[] NetworkIdentities = GameObject.FindObjectsOfType<NetworkIdentity>();
        foreach (NetworkIdentity netID in NetworkIdentities)
        {
            if (netID.netId == id)
            {
                return netID.gameObject;
            }
        }

        return null;
    }

    public static T FindGameObjectThroughNetID<T>(NetworkInstanceId id) where T : MonoBehaviour
    {
        return FindGameObjectThroughNetID(id).GetComponent<T>();
    }

    public static T GetComponentInHierarchy<T>(this GameObject go) where T : MonoBehaviour
    {
        T component = go.GetComponentInParent<T>();

        if (component != null)
        {
            component = go.GetComponentInChildren<T>();
        }

        return component;
    }

    public static T[] GetComponentsInHierarchy<T>(this GameObject go) where T : MonoBehaviour
    {
        List<T> components = new List<T>(go.GetComponentsInParent<T>());

        components.AddRange(go.GetComponentsInChildren<T>());

        return components.ToArray();
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = RNG.Next(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static float TrigToAngleRAD(float sine, float cosine)
    {
        double asin = Mathf.Asin(sine);
        double acos = Mathf.Acos(cosine);

        if (asin >= 0)
        {
            return (float)(acos);
        }
        else
        {
            return (float)((Mathf.PI * 2) - acos);
        }
    }

    public static float TrigToAngleDEG(float sine, float cosine)
    {
        return TrigToAngleRAD(sine, cosine) * RADTODEG;
    }

    public const float RADTODEG = (float)(180 / Mathf.PI);
}
