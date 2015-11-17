using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionRangeUtility {

    static bool debug = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //returns all Nodes that can be raycast in range
    public static List<NodeBehaviour> GetNodes (Pawn source, int range){
        List<NodeBehaviour> nodes = Pathfinder.ReturnAllNodes(source.currentNode);
        return nodes.FindAll(x => Vector3.Distance(source.transform.position, x.position) <= range);
    }

    //goes through all pawns that that can be 'seen' in 
    public static List<Pawn> GetPawns(Pawn source, int range)
    {   float time; 
        List <Pawn> pawnList = new List<Pawn>();
        if (debug) time = Time.realtimeSinceStartup;
        List<NodeBehaviour> nodes = GetNodes(source, range);
        foreach (NodeBehaviour node in nodes)
        {
            RaycastHit[] hits = Physics.RaycastAll(source.currentNode.offsetPosition, node.offsetPosition, range);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.GetComponent<Pawn>())
                {
                }
            }
        }
        return pawnList;
    }
}
