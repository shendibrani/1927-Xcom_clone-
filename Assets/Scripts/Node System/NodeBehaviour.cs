using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class NodeBehaviour : MonoBehaviour {
	
	public Vector3 position { get { return gameObject.transform.position; } }
	public Vector3 offsetPosition { get { return gameObject.transform.position + (Vector3.up * 0.5f); } }

	public static bool debug;

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

		if (Physics.Raycast (position, Vector3.forward, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<NodeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<NodeBehaviour> ());
			}
		}

		if (Physics.Raycast (position, Vector3.back, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<NodeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<NodeBehaviour> ());
			}
		}

		if (Physics.Raycast (position, Vector3.right, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<NodeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<NodeBehaviour> ());
			}
		}

		if (Physics.Raycast (position, Vector3.left, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<NodeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<NodeBehaviour> ());
			}
		}

		//////////////////////////////////////////////////

		if (Physics.Raycast (position, Vector3.forward + Vector3.up, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<SlopeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<SlopeBehaviour> ());
			}
		}
		
		if (Physics.Raycast (position, Vector3.back + Vector3.up, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<SlopeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<SlopeBehaviour> ());
			}
		}
		
		if (Physics.Raycast (position, Vector3.right + Vector3.up, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<SlopeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<SlopeBehaviour> ());
			}
		}
		
		if (Physics.Raycast (position, Vector3.left + Vector3.up, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<SlopeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<SlopeBehaviour> ());
			}
		}

		if (debug) Debug.Log ("Links: " + links.Count);
	}

	public void Bind (NodeBehaviour other){
		if (other != this){
			if (!links.Contains(other)){
				links.Add(other);
				if(other.links != null){
					other.links.Add(this);
				}
			}
		}
	}

	public void Unbind (NodeBehaviour other){
		if (debug) Debug.Log ("Unbinding");
		links.Remove(other);
	}

	public float NodeDistance(NodeBehaviour other)
	{	
		return Vector3.Distance(this.position, other.position);
	}

	protected virtual void OnDestroy() 
	{
		if (debug) Debug.Log ("Destroying");
		foreach (NodeBehaviour link in links){
			link.Unbind(this);
		}
	}

	public LinkPositions GetRelativePosition(NodeBehaviour node)
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

	public NodeBehaviour GetLinkInDirection (LinkPositions linkDir)
	{
		Vector3 direction;

		switch (linkDir){
		case LinkPositions.Forward: 
			direction = Vector3.forward;
			return (links.Find(x => x.position == position + direction));
			break;
		case LinkPositions.Right:
			direction = Vector3.right;
			return (links.Find(x => x.position == position + direction));
			break;
		case LinkPositions.Back:
			direction = Vector3.back;
			return (links.Find(x => x.position == position + direction));
			break;
		case LinkPositions.Left:
			direction = Vector3.left;
			return (links.Find(x => x.position == position + direction));
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
}

public enum LinkPositions{
	Forward, Right, Back, Left
}
