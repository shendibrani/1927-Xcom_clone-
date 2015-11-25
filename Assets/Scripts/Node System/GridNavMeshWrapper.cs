using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class GridNavMeshWrapper : MonoBehaviour
{
	[SerializeField] bool debug;
	
	bool stopped = true;
	
	public NodeBehaviour StartingNode;
	
	[SerializeField] Transform modelRoot;
	
	public Vector3 position 
	{ 
		get { return gameObject.transform.position; } 
		set { gameObject.transform.position = value; } 
	}
	
	NodeBehaviour currentDestination;

	NodeBehaviour _currentNode;
	public NodeBehaviour currentNode { 
		get { return _currentNode; } 
		private set {
			if(_currentNode != null){
				_currentNode.currentObject = null;
			}
			_currentNode = value;
			_currentNode.currentObject = GetComponent<Pawn>();
		}
	}
	
	public delegate void PathfinderEvent();
	
	public PathfinderEvent DestinationReached;
	
	void Start () 
	{
		position = StartingNode.offsetPosition;
		currentNode = StartingNode;
		currentDestination = StartingNode;
		DestinationReached += TurnManager.instance.SetFree;
	}
	
	void Update () 
	{
		if (!stopped) {
			if (ReachedDestination ()) {
				currentNode = currentDestination;
				//position = currentNode.offsetPosition;
				stopped = true;
				if (DestinationReached != null) {
					DestinationReached ();
				}
			}
			modelRoot.forward = GetComponent<NavMeshAgent> ().velocity.normalized;
			modelRoot.forward = new Vector3(modelRoot.forward.x, 0, modelRoot.forward.z);
		}
	}
	
	bool ReachedDestination()
	{
		return GetComponent<NavMeshAgent>().remainingDistance < 1f;
	}
	
	public void SetPath(List<NodeBehaviour> pPath){
		if (pPath != null && pPath.Count != 0){
			if (debug) Debug.Log ("Starting Pathing");
			GetComponent<NavMeshAgent>().destination = (pPath[pPath.Count-1].offsetPosition);
			currentDestination = pPath[pPath.Count-1];
			stopped = false;
		}
	}
}

