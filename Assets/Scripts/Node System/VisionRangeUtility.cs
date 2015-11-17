using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionRangeUtility
{
    static bool debug = true;

    //goes through all pawns that that can be 'seen' in range
    public static List<Pawn> GetPawns(Pawn source, int range)
    {
        /*float time = 0;
        if (debug) time = Time.realtimeSinceStartup;

        List<Pawn> pawns = new List<Pawn>(GameObject.FindObjectsOfType<Pawn>());
        pawns = pawns.FindAll(x => Vector3.Distance(source.transform.position, x.transform.position) <= range);

        List<Pawn> visibleList = new List<Pawn>();

        foreach (Pawn p in pawns)
        {
            RaycastHit[] hits = Physics.RaycastAll(source.transform.position, p.transform.position, range);
            if (debug) Debug.Log("Raycast hits " + hits.Length);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject.GetComponent<Pawn>())
                {
                    Pawn tmpPawn = hits[i].transform.gameObject.GetComponent<Pawn>();
                    visibleList.Add(tmpPawn);
                    if (!visibleList.Contains(tmpPawn)) visibleList.Add(tmpPawn);
                }
            }
        }
        if (debug) Debug.Log("Time to GetPawns: " + (Time.realtimeSinceStartup - time));
        if (debug) Debug.Log(visibleList.Count + " pawns found");
        return visibleList;
        */

        List<Pawn> potentialTargets = new List<Pawn>(GameObject.FindObjectsOfType<Pawn>());
        //Debug.Log(potentialTargets.Count);
        potentialTargets.RemoveAll(x => x.owner == source.owner);
        potentialTargets.RemoveAll(x => Vector3.Distance(source.transform.position, x.transform.position) > range);
        //Debug.Log(potentialTargets.Count);
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
        return actualTargets;
    }
}
