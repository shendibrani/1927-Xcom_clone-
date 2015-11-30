using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
[System.Serializable]
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
		set {
			if(_currentNode != null){
				_currentNode.currentObject = null;
			}
			_currentNode = value;
			_currentNode.currentObject = GetComponent<Targetable>();
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
		GetComponent<NavMeshAgent> ().updateRotation = false;
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
			UpdateRotation();
		}
	}
	
	bool ReachedDestination()
	{
		return GetComponent<NavMeshAgent> ().remainingDistance == 0 && GetComponent<NavMeshAgent> ().velocity.sqrMagnitude == 0;
	}
	
	public void SetPath(List<NodeBehaviour> pPath){
		if (pPath != null && pPath.Count != 0){
			if (debug) Debug.Log ("Starting Pathing");
			GetComponent<NavMeshAgent>().destination = (pPath[pPath.Count-1].offsetPosition);
			currentDestination = pPath[pPath.Count-1];
			stopped = false;
		}
	}

	void UpdateRotation ()
	{
		if(GetComponent<NavMeshAgent> ().velocity.normalized.sqrMagnitude !=0){
			modelRoot.forward = Vector3.Lerp(modelRoot.forward, GetComponent<NavMeshAgent> ().velocity.normalized, 0.5f);
		}
		modelRoot.forward = new Vector3(modelRoot.forward.x, 0, modelRoot.forward.z);
	}

}

