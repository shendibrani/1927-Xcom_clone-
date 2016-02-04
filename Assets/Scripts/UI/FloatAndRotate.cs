using UnityEngine;
using System.Collections;

public class FloatAndRotate : MonoBehaviour {

	float y;

	[SerializeField] float offset = 0.2f;
	[SerializeField] float rotSpeed = 30;

	// Use this for initialization
	void Start () {
		y = transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, y + Mathf.Sin(Time.time)*offset, transform.position.z);
		transform.Rotate ( new Vector3 (0, rotSpeed*Time.deltaTime, 0 ));	
	}
}
