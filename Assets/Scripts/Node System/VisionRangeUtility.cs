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
        List<NodeBehaviour> nodes = Pathfinder.ReturnAllNodes(source.currentNode);
        return nodes.FindAll(x => Vector3.Distance(source.transform.position, x.position) <= range);
    }

    //goes through all pawns that that can be 'seen' in range
    public static List<Pawn> GetPawns(Pawn source, int range)
    {
        float time = 0;
        if (debug) time = Time.realtimeSinceStartup;

        List<Pawn> pawnList = new List<Pawn>();
        List<NodeBehaviour> nodes = GetNodes(source, range);
        foreach (NodeBehaviour node in nodes)
        {
            RaycastHit[] hits = Physics.RaycastAll(source.currentNode.offsetPosition, node.offsetPosition, range);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.gameObject.GetComponent<Pawn>())
                {
                    Pawn tmpPawn = hits[i].transform.gameObject.GetComponent<Pawn>();
                    pawnList.Add(tmpPawn);
                    //if (!pawnList.Contains(tmpPawn)) pawnList.Add(tmpPawn);
                }
            }
        }
        if (debug) Debug.Log("Time to GetPawns: " + (Time.realtimeSinceStartup - time));
        if (debug) Debug.Log(pawnList.Count + " pawns found");
        return pawnList;
    }
}
