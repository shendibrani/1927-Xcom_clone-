using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(Targetable))]
public class NodeBehaviour : MonoBehaviour {
	
	public Vector3 position { get { return gameObject.transform.position; } }
	public Vector3 offsetPosition { get { return gameObject.transform.position + (Vector3.up * 0.5f); } }

	public static bool debug;

    public bool isOccupied { get { return (currentObject != null); } }

	private Targetable _currentObject;
    public Targetable currentObject {
		get{ return _currentObject;}
		set {
			if (_currentObject is Pawn) {
				AddOnLeaveEffect(_currentObject.GetComponent<Pawn>());
			}
			_currentObject = value;
			if (_currentObject is Pawn) {
				AddOnEnterEffect(_currentObject.GetComponent<Pawn>());
			}
		}
	}

	public PawnEffect tileEffect { get; protected set;}

	public List<NodeBehaviour> links { get; protected set; }
	
	protected virtual void Start () 
	{
		if(debug) Debug.Log ("Exist");

		NodeSetup ();
	}

	public virtual void NodeSetup ()
	{
		links = new List<NodeBehaviour> ();
		RaycastHit hit = new RaycastHit ();

		FindNodeInDirectionAndBind (Vector3.forward, ref hit);
		FindNodeInDirectionAndBind (Vector3.right, ref hit);
		FindNodeInDirectionAndBind (Vector3.back, ref hit);
		FindNodeInDirectionAndBind (Vector3.left, ref hit);

		if (debug) Debug.Log ("Links: " + links.Count);
	}

	void FindNodeInDirectionAndBind (Vector3 direction, ref RaycastHit hit)
	{
		if (Physics.Raycast (position, direction, out hit, 1)) {
			if (hit.collider.gameObject.GetComponent<NodeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<NodeBehaviour> ());
			}
		} else if (Physics.Raycast (position, direction + Vector3.up, out hit, 1)) {
			if (hit.collider.gameObject.GetComponent<SlopeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<SlopeBehaviour> ());
			}
		} else if (Physics.Raycast (position + direction, Vector3.down, out hit, 3)) {
			if (hit.collider.gameObject.GetComponent<NodeBehaviour> () != null) {
				OneWayBind (hit.collider.gameObject.GetComponent<NodeBehaviour> ());
			}
		}
	}

	public void Bind (NodeBehaviour other)
	{
		OneWayBind (other);

		if(other.links != null){
			other.OneWayBind(this);
		}
	}

	public void OneWayBind(NodeBehaviour other)
	{
		if (other != this){
			if (!links.Contains(other)){
				links.Add(other);
			}
		}
	}

	public void Unbind (NodeBehaviour other)
	{
		if (debug) Debug.Log ("Unbinding");
		links.Remove(other);
		other.links.Remove (this);
	}

	public float NodeDistance(NodeBehaviour other)
	{	
		return Vector3.Distance(this.position, other.position);
	}

	protected virtual void OnDestroy() 
	{
		if (debug) Debug.Log ("Destroying");
		for (int counter = links.Count -1; counter >= 0; counter--){
			Unbind(links[counter]);
		}
	}

	public LinkPositions GetRelativePositionInLinks(NodeBehaviour node)
	{
		if(links.Contains(node)){
			Vector3 offset = node.position - position;
			offset = new Vector3 (offset.x, 0, offset.z);
			offset.Normalize ();

			if (Vector3.Dot (offset, Vector3.forward) == 1) {
				return LinkPositions.Forward;
			} else if (Vector3.Dot (offset, Vector3.right) == 1) {
				return LinkPositions.Right;
			} else if (Vector3.Dot (offset, Vector3.back) == 1) {
				return LinkPositions.Back;
			} else if (Vector3.Dot (offset, Vector3.left) == 1) {
				return LinkPositions.Left;
			}
		}

		throw new UnityException ("Nodes not aligned");
	}

    public LinkPositions GetRelativePosition(NodeBehaviour node)
    {
            Vector3 offset = node.position - position;
            offset = new Vector3(offset.x, 0, offset.z);
            offset.Normalize();

            if (Vector3.Dot(offset, Vector3.forward) == 1)
            {
                return LinkPositions.Forward;
            }
            else if (Vector3.Dot(offset, Vector3.right) == 1)
            {
                return LinkPositions.Right;
            }
            else if (Vector3.Dot(offset, Vector3.back) == 1)
            {
                return LinkPositions.Back;
            }
            else if (Vector3.Dot(offset, Vector3.left) == 1)
            {
                return LinkPositions.Left;
            }
            throw new UnityException("Nodes not aligned");
    }

	public NodeBehaviour GetLinkInDirection (LinkPositions linkDir)
	{
		Vector3 direction;

		switch (linkDir) {
		case LinkPositions.Forward: 
			direction = Vector3.forward;
			return (links.Find (x => x.position == position + direction));
			break;
		case LinkPositions.Right:
			direction = Vector3.right;
			return (links.Find (x => x.position == position + direction));
			break;
		case LinkPositions.Back:
			direction = Vector3.back;
			return (links.Find (x => x.position == position + direction));
			break;
		case LinkPositions.Left:
			direction = Vector3.left;
			return (links.Find (x => x.position == position + direction));
			break;
		}

		return null;
	}

	void OnDrawGizmos()
	{
		if (links == null) {
			return;
		}
		Gizmos.color = Color.blue;
		foreach (NodeBehaviour node in links) {
			Gizmos.DrawLine(position, node.position);
		}
	}

	void AddOnEnterEffect (Pawn target)
	{
		
	}

	void AddOnLeaveEffect(Pawn target)
	{

	}
}

public enum LinkPositions{
	Forward, Right, Back, Left
}
