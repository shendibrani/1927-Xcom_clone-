using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineOfSightManager : MonoBehaviour
{
	[SerializeField] bool debug;

	List<Pawn> dirtyPawns;

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

	Dictionary<Pawn, List<Pawn>> sightMap;
	List<VisibleBasedOnLoS> seeingPawns;

	void Start()
	{
		sightMap 		= new Dictionary<Pawn, List<Pawn>> ();

		seeingPawns 	= new List<VisibleBasedOnLoS> (VisibleBasedOnLoS.all);
		seeingPawns 	= seeingPawns.FindAll(x => x.generatesLineOfSight);

		dirtyPawns 		= new List<Pawn>(Pawn.all);

		foreach (Pawn p in Pawn.all) {
			sightMap.Add(p,new List<Pawn>());
		}
	}

	void FixedUpdate ()
	{
		foreach (Pawn p in dirtyPawns) {
			UpdatePawnLoS (p);
		}

		dirtyPawns.Clear ();

		foreach(VisibleBasedOnLoS s in VisibleBasedOnLoS.all)
		{
			s.OutOfHearingRange();
		}

		foreach (VisibleBasedOnLoS p in seeingPawns) 
		{
			foreach (VisibleBasedOnLoS s in VisibleBasedOnLoS.all) {
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

	void UpdatePawnLoS (Pawn p)
	{
		sightMap[p].Clear ();

		foreach(List<Pawn> sl in sightMap.Values){
			sl.Remove(p);
		}

		for (int counter = 0; counter < Pawn.all.Count - 1; counter++) {
			if (CheckSight (Pawn.all [counter], p)) {
				sightMap [Pawn.all [counter]].Add (p);
				sightMap [p].Add (Pawn.all [counter]);
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

	public void SetPawnDirty(Pawn p)
	{
		if(!dirtyPawns.Contains(p)) dirtyPawns.Add (p);
	}

	public void StopGeneratingLoS(VisibleBasedOnLoS seeingPawn)
	{
		seeingPawns.Remove (seeingPawn);
	}
}

