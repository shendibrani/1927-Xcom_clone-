using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionRangeUtility
{
    static bool debug = true;

    //goes through all pawns that that can be 'seen' in range
	public static List<Pawn> GetPawns(Pawn source, int range)
	{
        List<Pawn> potentialTargets = new List<Pawn>(GameObject.FindObjectsOfType<Pawn>());
        Debug.Log(potentialTargets.Count);
        potentialTargets.RemoveAll(x => x.owner == source.owner);
        potentialTargets.RemoveAll(x => Vector3.Distance(source.transform.position, x.transform.position) > range);
        Debug.Log(potentialTargets.Count);
        List<Pawn> actualTargets = new List<Pawn>();

        RaycastHit hit = new RaycastHit();
        foreach (Pawn target in potentialTargets)
        {
            Physics.Raycast(
                source.transform.position + (Vector3.up * 0.1f),
                (target.transform.position - source.transform.position).normalized,
                out hit);
            Debug.Log(hit);
            Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == target.gameObject)
                {
                    actualTargets.Add(target);
                }
            }
        }

		Debug.Log(actualTargets.Count);
        return actualTargets;
    }

	public static List<Pawn> GetPawns(Pawn source){
		return GetPawns (source, source.sightRange);
	}
}
