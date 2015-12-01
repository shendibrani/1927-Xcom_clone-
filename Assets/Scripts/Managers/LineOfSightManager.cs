using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineOfSightManager : MonoBehaviour
{
	public static LineOfSightManager instance {
		get {
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<LineOfSightManager>();
			}
			return _instance;
		}
	}

	static Vector3 verticalOffset = Vector3.up * 1.5f;
	static Vector3[] corners {
		get {
			Vector3[] temp = new Vector3[4];
			temp[0] = new Vector3(0.49f, 0, 0.49f);
			temp[1] = new Vector3(0.49f, 0, -0.49f);
			temp[2] = new Vector3(-0.49f, 0, -0.49f);
			temp[3] = new Vector3(-0.49f, 0, 0.49f);
			return temp;
		}
	}
	
	private static LineOfSightManager _instance;

	List<Pawn> pawns;

	Dictionary<Pawn, List<Pawn>> sightMap;
	Dictionary<Pawn, bool> redundancyList;

	void Start()
	{
		pawns = new List<Pawn>(GameObject.FindObjectsOfType<Pawn> ());
		redundancyList = new Dictionary<Pawn, bool> ();
		sightMap = new Dictionary<Pawn, List<Pawn>> ();

		foreach (Pawn p in pawns) {
			redundancyList.Add(p,false);
			sightMap.Add(p,new List<Pawn>());
		}
	}

	void FixedUpdate ()
	{
		foreach (List<Pawn> l in sightMap.Values) {
			l.Clear();
		}

		for (int counter = 0; counter < pawns.Count-1; counter++) {
			for (int matcher = counter+1; matcher < pawns.Count; matcher++){
				if(CheckSight(pawns[counter], pawns[matcher])){
					sightMap[pawns[counter]].Add(pawns[matcher]);
					sightMap[pawns[matcher]].Add(pawns[counter]);
				}
			}
		}
	}

	public static bool CheckSight(Pawn a, Pawn b)
	{
		if (Vector3.Distance (a.transform.position, b.transform.position) <= Pawn.sightRange) {
			//Debug.Log("Distance Checks out");
			RaycastHit hit;
			foreach(Vector3 corner in corners){
				Physics.Raycast(a.transform.position + verticalOffset + corner, 
				                b.transform.position - a.transform.position,
				                out hit);
				if(hit.collider != null && hit.collider.GetComponent<Pawn>() == b){
					//Debug.Log("LoS Checks out to "+hit.collider.name);
					if (a.GetComponent<VisibleBasedOnLoS> () != null) {
						a.GetComponent<VisibleBasedOnLoS> ().Visible();
					}
					if (b.GetComponent<VisibleBasedOnLoS> () != null) {
						b.GetComponent<VisibleBasedOnLoS> ().Visible();
					}
					return true;
				}
			}
			if (a.GetComponent<VisibleBasedOnLoS> () != null) {
				a.GetComponent<VisibleBasedOnLoS> ().Hidden();
			}
			if (b.GetComponent<VisibleBasedOnLoS> () != null) {
				b.GetComponent<VisibleBasedOnLoS> ().Hidden();
			}
			return false;
		}
		if (a.GetComponent<VisibleBasedOnLoS> () != null) {
			a.GetComponent<VisibleBasedOnLoS> ().OutOfHearingRange();
		}
		if (b.GetComponent<VisibleBasedOnLoS> () != null) {
			b.GetComponent<VisibleBasedOnLoS> ().OutOfHearingRange();
		}
		return false;
	}

	public static bool CheckSight(Pawn a, DestroyableProp b)
	{
		if (Vector3.Distance (a.transform.position, b.transform.position) <= Pawn.sightRange) {
			//Debug.Log("Distance Checks out");
			RaycastHit hit;
			foreach(Vector3 corner in corners){
				Physics.Raycast(a.transform.position + verticalOffset + corner, 
				                b.transform.position - (a.transform.position + verticalOffset),
				                out hit);
				if(hit.collider != null && hit.collider.GetComponent<DestroyableProp>() == b){
					//Debug.Log("LoS Checks out to "+hit.collider.name);
					return true;
				}
			}
		}
		return false;
	}

	public static List<Pawn> GetSightList(Pawn p)
	{
		return instance.sightMap [p];
	}
}

