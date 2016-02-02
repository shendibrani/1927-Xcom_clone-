using UnityEngine;
using System.Collections;

public class FlythroughScript : MonoBehaviour {

	int targetIndex = 0;

	[SerializeField] [Range(0.0f, 1.0f)] float lerpFactor;
	[SerializeField] Transform[] waypoints;
	[SerializeField] bool startAtPosZero;

	// Use this for initialization
	void Start () {
		if (waypoints.Length >= 1 && startAtPosZero) {
			transform.position = waypoints[0].position;
			transform.rotation = waypoints[0].rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, waypoints[targetIndex].position, lerpFactor);
		transform.rotation = Quaternion.Lerp(transform.rotation, waypoints[targetIndex].rotation, lerpFactor);
	}

	public void Next(){
		targetIndex++;
		targetIndex = (targetIndex%waypoints.Length + waypoints.Length)%waypoints.Length;
	}

	public void Prev(){
		targetIndex--;
		targetIndex = (targetIndex%waypoints.Length + waypoints.Length)%waypoints.Length;
	}
}
