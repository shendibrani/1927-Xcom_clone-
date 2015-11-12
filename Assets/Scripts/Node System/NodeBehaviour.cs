using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class NodeBehaviour : MonoBehaviour {
	
	public Vector3 position { get { return gameObject.transform.position; } }
	public Vector3 offsetPosition { get { return gameObject.transform.position + Vector3.up; } }

	public List<NodeBehaviour> Links { get; private set; }
	
	protected virtual void Start () 
	{
		//Debug.Log ("Exist");

		NodeSetup ();
	}

	public void NodeSetup ()
	{
		Links = new List<NodeBehaviour> ();
		RaycastHit hit = new RaycastHit ();
		foreach (NodeBehaviour node in FindObjectsOfType<NodeBehaviour> ()) {
			if (Physics.Raycast (position, (node.position - position).normalized, out hit)) {
				if (hit.collider.gameObject.GetComponent<NodeBehaviour> () == node) {
					Bind (node);
				}
			}
		}
	}

	public void Bind (NodeBehaviour other){
		if (other != this){
			if (!Links.Contains(other)){
				Links.Add(other);
				if(other.Links != null){
					other.Links.Add(this);
				}
			}
		}
	}

	public void Unbind (NodeBehaviour other){
		Links.Remove(other);
	}

	public float NodeDistance(NodeBehaviour other)
	{	
		return Vector3.Distance(this.position, other.position);
	}

	protected virtual void OnDestroy() {
		foreach (NodeBehaviour link in Links){
			link.Unbind(this);
		}
	}

	void OnDrawGizmos()
	{
		if (Links == null) {
			return;
		}
		Gizmos.color = Color.blue;
		foreach (NodeBehaviour node in Links) {
			Gizmos.DrawLine(position, node.position);
		}
	}
}
