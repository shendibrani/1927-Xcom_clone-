using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class NodeBehaviour : MonoBehaviour {
	
	public Vector3 position { get { return gameObject.transform.position; } }
	public Vector3 offsetPosition { get { return gameObject.transform.position + Vector3.up; } }

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

//		foreach (NodeBehaviour node in FindObjectsOfType<NodeBehaviour> ()) {
//			if (Physics.Raycast (position, (node.position - position).normalized, out hit)) {
//				if (hit.collider.gameObject.GetComponent<NodeBehaviour> () == node) {
//					Bind (node);
//				}
//			}
//		}

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
