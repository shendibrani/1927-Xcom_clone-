using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMovementBehaviour : MonoBehaviour
{
	List<NodeBehaviour> path;

	[SerializeField] bool debug;

	bool stopped = true;
	
	public NodeBehaviour StartingNode;

	[SerializeField] Transform modelRoot;

	public Vector3 position { 
		get { return gameObject.transform.position; } 
		set { gameObject.transform.position = value; } 
	}
	
	NodeBehaviour currentDestination;
	[HideInInspector] public NodeBehaviour currentNode;
	
	[SerializeField] float Speed;
	
	Vector3 velocity;

	public delegate void PathfinderEvent();

	public PathfinderEvent DestinationReached;

	void Start () 
	{
		position = StartingNode.offsetPosition;
		currentNode = StartingNode;
		currentDestination = StartingNode;
		DestinationReached += TurnManager.instance.SetFree;
	}
	
	void Update () {
		if (!stopped) {
			if (ReachedCurrentDestination ()) {
				currentNode = currentDestination;
				position = currentNode.offsetPosition;
				if (path == null) {
					stopped = true;
					if (DestinationReached != null) {
						DestinationReached ();
					}
				} else {
					NextDestination ();
				}
			} else {
				position += velocity;
			}
		}
		modelRoot.LookAt (currentDestination.offsetPosition);
	}
	
	bool ReachedCurrentDestination(){
		return Vector3.Distance (position, currentDestination.offsetPosition) < Speed;
	}
	
	void NextDestination(){
		
		if (path == null){return;}

		if (debug) Debug.Log (path.Count);

		currentDestination = path[0];
		path.RemoveAt(0);
		if (debug) Debug.Log (path.Count);
		if (path.Count == 0){
			if (debug) Debug.Log ("Nulling Path");
			path = null;
		}
		velocity = currentDestination.offsetPosition - position;
		velocity.Normalize ();
		velocity *= Speed;
		if (debug) Debug.Log (velocity);
	}

	public void SetPath(List<NodeBehaviour> pPath){
		path = pPath;

		if (path != null && path.Count != 0){
			if (debug) Debug.Log ("Starting Pathing");
			stopped = false;
			NextDestination();
		}
	}
}

