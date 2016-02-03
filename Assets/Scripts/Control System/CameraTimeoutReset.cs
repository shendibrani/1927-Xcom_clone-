using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraViewController))]
public class CameraTimeoutReset : MonoBehaviour
{
	[SerializeField] float resetTime;
	[SerializeField] [Range(0.0f, 1.0f)] float lerpFactor;
	float timer;
	bool resetting;

	// Use this for initialization
	void Start ()
	{
		timer = resetTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GetComponent<CameraViewController> ().hasMoved) {
			timer -= Time.deltaTime;
		} else {
			timer = resetTime;
		}

		if (timer <= 0) {
			CameraReset();
		}

		if (resetting) {
			transform.position = Vector3.Lerp(transform.position, SelectionManager.selected.transform.position, lerpFactor);
			transform.rotation = Quaternion.Lerp(transform.rotation, SelectionManager.selected.transform.rotation, lerpFactor);
		}
	}

	void CameraReset()
	{
		if (SelectionManager.selected != null) {
			resetting = true;
			timer = resetTime;
		}
	}
}

