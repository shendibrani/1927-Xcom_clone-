using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder
{
	static bool debug = false;

	public static List<NodeBehaviour> GetPath (NodeBehaviour start, NodeBehaviour target)
	{
		List<NodeBehaviour> path = new List<NodeBehaviour>();

		//return empty list if start is target;
		if(start == target){
			return path;
		}
		
		Dictionary<NodeBehaviour, NodeBehaviour> parentTable = new Dictionary<NodeBehaviour, NodeBehaviour>();
		
		List<NodeBehaviour> testing = new List<NodeBehaviour>();
		List<NodeBehaviour> tested 	= new List<NodeBehaviour>();
		
		parentTable.Add(start, null);
		
		NodeBehaviour currentNode = start;
		
		int counter = 0;
		
		do {
			tested.Add(currentNode);
			
			foreach (NodeBehaviour node in currentNode.Links){
				if(!tested.Contains(node)){
					if(debug) Debug.Log("Added node to testing");
					testing.Add(node);
				}
			}
			
			foreach (NodeBehaviour node in testing){
				
				if (node == target) {
					if(debug) Debug.Log("Found Goal");
					parentTable[node] = currentNode;
					
					NodeBehaviour tempNode = node;
					while (tempNode != null) {
						if(debug) Debug.Log("Add to path");
						path.Add(tempNode);
						tempNode = parentTable[tempNode];
					}
					
					path.Reverse();
					
					return path;
				}
				
				if(!parentTable.ContainsKey(node)){
					if(debug) Debug.Log("New table entry");
					parentTable[node] = currentNode;
				} else if (parentTable.ContainsKey(node) &&
				           EstimatedTotalCost(target, node, parentTable) > 
				           EstimatedTotalCost(target, node, currentNode, parentTable))
				{
					if(debug) Debug.Log("Updating parent entry");
					parentTable[node] = currentNode;
				}
			}
			
			if(debug) Debug.Log("Moving node from testing to tested");
			testing.Remove(currentNode);
			tested.Add(currentNode);
			
			testing.Sort((x,y) => EstimatedTotalCost(target, x, parentTable).CompareTo(EstimatedTotalCost(target, y, parentTable)));
			
			currentNode = testing[0];
			
			counter++;
		} while (testing.Count != 0/* && counter < 100*/);
		
		return null;
	}
	
	
	
	public static float EstimatedTotalCost (NodeBehaviour target, NodeBehaviour evaluatedNode, Dictionary<NodeBehaviour, NodeBehaviour> parentTable)
	{
		float estimateRemainingDistance = Vector3.Distance(evaluatedNode.position, target.position);
		
		return PathLength(evaluatedNode, parentTable) + estimateRemainingDistance;
	}
	
	public static float EstimatedTotalCost (NodeBehaviour target, NodeBehaviour evaluatedNode, NodeBehaviour alternateParent, Dictionary<NodeBehaviour, NodeBehaviour> parentTable)
	{
		float estimateRemainingDistance = Vector3.Distance(evaluatedNode.position, target.position);
		
		return PathLength(evaluatedNode, alternateParent, parentTable) + estimateRemainingDistance;
	}
	
	public static float PathLength(NodeBehaviour node, Dictionary<NodeBehaviour, NodeBehaviour> parentTable){
		
		if (node == null){
			throw new UnityException("Node is null");
		}
		
		float total = 0;
		
		if (!parentTable.ContainsKey(node)){
			return total;
		}
		
		while (!parentTable.ContainsKey(node)){
			total += Vector3.Distance(node.position, parentTable[node].position);
			node = parentTable[node];
		}
		
		return total;
	}
	
	public static float PathLength(NodeBehaviour node, NodeBehaviour alternateParent, Dictionary<NodeBehaviour, NodeBehaviour> parentTable){
		if (node == null){
			throw new UnityException("Node is null");
		}
		
		if (alternateParent == null){
			throw new UnityException("Alternate parent is null");
		}
		
		float total = Vector3.Distance(node.position, alternateParent.position);
		
		total += PathLength(alternateParent, parentTable);
		
		return total;
	}

	public static List<NodeBehaviour> FindNodesWithinSteps(NodeBehaviour start, int steps){

		List<NodeBehaviour> nodes = new List<NodeBehaviour> ();

		nodes.Add (start);

		if (steps == 1) {
			nodes.AddRange (start.Links);
			return nodes;
		} else {
			nodes.AddRange (FindNodesWithinSteps (start, steps - 1));
			return nodes;
		}
	}
}

