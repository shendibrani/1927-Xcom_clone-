using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMovementBehaviour : MonoBehaviour
{
	List<NodeBehaviour> path;

	[SerializeField] bool debug;

	bool stopped = true;
	
	public NodeBehaviour StartingNode;
	
	public Vector3 position { 
		get { return gameObject.transform.position; } 
		set { gameObject.transform.position = value; } 
	}
	
	NodeBehaviour currentDestination;
	public NodeBehaviour currentNode { get; private set; }
	
	[SerializeField] float Speed;
	
	Vector3 velocity;

	public delegate void PathfinderEvent();

	public PathfinderEvent DestinationReached;

	// Use this for initialization
	void Start () 
	{
		position = StartingNode.offsetPosition;
		currentNode = StartingNode;
		currentDestination = StartingNode;
	}
	
	// Update is called once per frame
	void Update () {
		if (!stopped) {
			if (ReachedCurrentDestination ()) {
				if (path == null) {
					stopped = true;
					if (DestinationReached != null) {
						DestinationReached ();
					}
				}
				currentNode = currentDestination;
				NextDestination ();
			} else {
				position += velocity;
			}
		}
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

