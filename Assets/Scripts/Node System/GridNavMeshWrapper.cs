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
			GetComponentInChildren<Animator>().SetBool("Moving", GetComponent<NavMeshAgent>().isOnNavMesh);
			GetComponentInChildren<Animator>().SetBool("Air", GetComponent<NavMeshAgent>().isOnOffMeshLink);
			if (ReachedDestination ()) {
				currentNode = currentDestination;
				//position = currentNode.offsetPosition;
				stopped = true;
				if (DestinationReached != null) {
					DestinationReached ();
					GetComponentInChildren<Animator>().SetBool("Moving",false);
					GetComponentInChildren<Animator>().SetBool("Air", false);
					UpdateCurrentCoverState();
				}
			}
		}
		UpdateRotation();
	}
	
	bool ReachedDestination()
	{
		if (debug) Debug.Log ("Reached Destination: " + (!GetComponent<NavMeshAgent> ().pathPending && GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete && GetComponent<NavMeshAgent> ().remainingDistance == 0));
		if (debug) Debug.Log ("PathPending: " + (!GetComponent<NavMeshAgent> ().pathPending));
		if (debug) Debug.Log ("PathStatus == complete: " + (GetComponent<NavMeshAgent> ().pathStatus == NavMeshPathStatus.PathComplete));
		if (debug) Debug.Log ("Remainingistance == 0: " + (GetComponent<NavMeshAgent> ().remainingDistance == 0));

		return (!GetComponent<NavMeshAgent> ().pathPending && GetComponent<NavMeshAgent> ().remainingDistance == 0);
	}
	
	public void SetPath(List<NodeBehaviour> pPath){
		if (pPath != null && pPath.Count != 0){
			if (debug) Debug.Log ("Starting Pathing");
			GetComponent<NavMeshAgent>().destination = (pPath[pPath.Count-1].offsetPosition);
			currentDestination = pPath[pPath.Count-1];
			if (debug) Debug.Log ("Remaining Distance at pathing start: " + GetComponent<NavMeshAgent> ().remainingDistance);
			stopped = false;
			GetComponentInChildren<Animator>().SetBool("Moving", true);
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

    void UpdateCurrentCoverState()
	{
		CoverState cs = CoverState.Full;

		List<Pawn> enemies = GetComponent<Pawn>().sightList.FindAll(x => x.owner != GetComponent<Pawn>().owner);

		GetComponentInChildren<Animator>().SetBool("Ducking",false);

		foreach (Pawn e in enemies) {
			if (Pawn.GetCoverAtNode(GetComponent<Pawn>().currentNode, e) == CoverState.Half){
				GetComponentInChildren<Animator>().SetBool("Ducking",true);
			}
		}
	}
}

