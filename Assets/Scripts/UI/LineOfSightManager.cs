using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineOfSightManager : MonoBehaviour
{
	[SerializeField] bool debug;

	public static LineOfSightManager instance {
		get {
			if(_instance == null){
				_instance = GameObject.FindObjectOfType<LineOfSightManager>();
				if(_instance == null){
					Debug.LogError("There is not Line of Sight manager instance in the scene.");
				}
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
	//Dictionary<Pawn, bool> redundancyList;

	void Start()
	{
		pawns = new List<Pawn>(GameObject.FindObjectsOfType<Pawn> ());
		//redundancyList = new Dictionary<Pawn, bool> ();
		sightMap = new Dictionary<Pawn, List<Pawn>> ();

		foreach (Pawn p in pawns) {
			//redundancyList.Add(p,false);
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

		List<VisibleBasedOnLoS> seeingPawns = new List<VisibleBasedOnLoS> (FindObjectsOfType<VisibleBasedOnLoS> ());
		seeingPawns = seeingPawns.FindAll(x => x.generatesLineOfSight);

		foreach(VisibleBasedOnLoS s in FindObjectsOfType<VisibleBasedOnLoS>())
		{
			s.OutOfHearingRange();
		}

		foreach (VisibleBasedOnLoS p in seeingPawns) 
		{
			foreach (VisibleBasedOnLoS s in FindObjectsOfType<VisibleBasedOnLoS>()) {
				if (Vector3.Distance (p.transform.position, s.transform.position) <= Pawn.sightRange) {
					s.Hidden ();
					break;
				}
			}
		}

		foreach (VisibleBasedOnLoS p in seeingPawns) 
		{
			foreach (Pawn s in sightMap[p.GetComponent<Pawn>()]) {
				s.GetComponent<VisibleBasedOnLoS>().Visible();
			}
		}
	}

	public static bool CheckSight(Pawn a, Pawn b)
	{
		if (Vector3.Distance (a.transform.position, b.transform.position) <= Pawn.sightRange) {
			if (instance.debug) Debug.Log("Distance Checks out");
			RaycastHit hit;
			foreach(Vector3 corner in corners){
				Physics.Raycast(a.transform.position + verticalOffset + corner, 
				                b.transform.position - a.transform.position,
				                out hit);
				if(hit.collider != null && hit.collider.GetComponent<Pawn>() == b){
					if (instance.debug) Debug.Log("LoS Checks out to "+hit.collider.name);
					return true;
				}
			}
		}
		return false;
	}

	public static bool CheckSight(Pawn a, DestroyableProp b)
	{
		if (Vector3.Distance (a.transform.position, b.transform.position) <= Pawn.sightRange) {
			if (instance.debug) Debug.Log("Distance Checks out");
			RaycastHit hit;
			foreach(Vector3 corner in corners){
				Physics.Raycast(a.transform.position + verticalOffset + corner, 
				                b.transform.position - (a.transform.position + verticalOffset),
				                out hit);
				if(hit.collider != null && hit.collider.GetComponent<DestroyableProp>() == b){
					if (instance.debug) Debug.Log("LoS Checks out to "+hit.collider.name);
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

