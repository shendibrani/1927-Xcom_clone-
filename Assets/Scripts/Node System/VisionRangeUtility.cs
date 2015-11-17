using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionRangeUtility
{

    static bool debug = true;

    //returns all Nodes that can be raycast in range
    public static List<NodeBehaviour> GetNodes(Pawn source, int range)
    {
        if (debug) Debug.Log("Gets all nodes");
        List<NodeBehaviour> nodes = new List<NodeBehaviour>(GameObject.FindObjectsOfType<NodeBehaviour>());
        if (debug) Debug.Log("Returned " + nodes.Count + " nodes");
        return nodes.FindAll(x => Vector3.Distance(source.transform.position, x.position) <= range);
    }

    //goes through all pawns that that can be 'seen' in range
    public static List<Pawn> GetPawns(Pawn source, int range)
    {
        float time = 0;
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
    }
}
