using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
[System.Serializable]
public class GridNavMeshWrapper : MonoBehaviour
{
	[SerializeField] bool debug;
    [SerializeField] bool automaticFindNode = true;
	
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
        if (automaticFindNode)
        {
            RaycastToStartingNode();
        }
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
		}
		UpdateRotation();
	}
	
	bool ReachedDestination()
	{
		if (debug) Debug.Log ("Reached Destination: " + (!GetComponent<NavMeshAgent> ().pathPending && GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete && GetComponent<NavMeshAgent> ().remainingDistance == 0 && GetComponent<NavMeshAgent> ().velocity.sqrMagnitude == 0));
		return (!GetComponent<NavMeshAgent> ().pathPending && GetComponent<NavMeshAgent> ().remainingDistance == 0 && GetComponent<NavMeshAgent> ().velocity.sqrMagnitude == 0);
	}
	
	public void SetPath(List<NodeBehaviour> pPath){
		if (pPath != null && pPath.Count != 0){
			if (debug) Debug.Log ("Starting Pathing");
			GetComponent<NavMeshAgent>().destination = (pPath[pPath.Count-1].offsetPosition);
			currentDestination = pPath[pPath.Count-1];
			if (debug) Debug.Log ("Remaining Distance at pathing start: " + GetComponent<NavMeshAgent> ().remainingDistance);
			stopped = false;
		}
	}

	void UpdateRotation ()
	{
		if (debug) Debug.Log (GetComponent<NavMeshAgent> ().velocity.normalized);
		if(GetComponent<NavMeshAgent> ().velocity.normalized.magnitude !=0){
			modelRoot.forward = Vector3.Lerp(modelRoot.forward, GetComponent<NavMeshAgent> ().velocity.normalized, 0.5f);
		}
		modelRoot.forward = new Vector3(modelRoot.forward.x, 0, modelRoot.forward.z);
	}

    void RaycastToStartingNode()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, 0.1f, Vector3.down, 3f);

        foreach (RaycastHit r in hit)
        {
            if (r.transform.GetComponent<NodeBehaviour>() != null)
            {
                StartingNode = r.collider.GetComponent<NodeBehaviour>();
                position = StartingNode.offsetPosition;
                break;
            }
        }
    }

	public void SetModelRoot(Transform _object)
	{
		modelRoot = _object;
	}

    
}

