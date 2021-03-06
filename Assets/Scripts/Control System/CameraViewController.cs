﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(VerticalEdgePanAxis))]
[RequireComponent(typeof(HorizontalEdgePanAxis))]
[RequireComponent(typeof(MouseRotateAxis))]
public class CameraViewController : MonoBehaviour {

    VerticalEdgePanAxis verticalAxis;
    HorizontalEdgePanAxis horizontalAxis;
	MouseRotateAxis rotateAxis;
	MouseWheelAxis scrollAxis;

	[SerializeField] Transform topLeft,bottomRight;

	[SerializeField] float panSpeed = 0.5f;
	[SerializeField] float rotateSpeed = 36f;

	[SerializeField] float minDistance = 5f;
	[SerializeField] float maxDistance = 30f;

	public bool hasMoved { get; private set; }

	// Use this for initialization
	void Start () 
	{
        verticalAxis = GetComponent<VerticalEdgePanAxis>();
        horizontalAxis = GetComponent<HorizontalEdgePanAxis>();
		rotateAxis = GetComponent<MouseRotateAxis> ();
		scrollAxis = GetComponent<MouseWheelAxis> ();

		Camera.main.transform.localPosition = Camera.main.transform.forward * -1 * minDistance;
	}
	
	// Update is called once per frame
	void Update () 
	{
		hasMoved = false;
		if (verticalAxis.axisValue != 0f || 
		    horizontalAxis.axisValue != 0f || 
		    rotateAxis.axisValue != 0f || 
		    scrollAxis.axisValue != 0f) {
			hasMoved = true;
		}

        if (Input.GetMouseButton(2))
        {
            RotateCamera();
        }
        else
        {
            SlideCamera();
        }
		ZoomCamera ();

		ComputeBorders();
	}

    //move camera according to mouse offest in axis values according to dolly rotation
    void SlideCamera()
    {
		Vector3 offset = (transform.right * horizontalAxis.axisValue + transform.forward * verticalAxis.axisValue) * panSpeed * Time.deltaTime;

        transform.position += offset;
    }

    void RotateCamera()
    {
        transform.Rotate(0, rotateAxis.axisValue * rotateSpeed * Time.deltaTime, 0);
    }

	void ZoomCamera ()
	{
		if ((Vector3.Distance (Camera.main.transform.localPosition, Vector3.zero) <= minDistance && scrollAxis.axisValue > 0) ||
		    (Vector3.Distance (Camera.main.transform.localPosition, Vector3.zero) >= maxDistance) && scrollAxis.axisValue < 0) {
			return;
		}
		Camera.main.transform.position += Camera.main.transform.forward * scrollAxis.axisValue;
	}

    public void RotateCamera(Vector3 pRotation)
    {
        transform.Rotate(pRotation);
    }

	void ComputeBorders ()
	{
		transform.position = new Vector3 (Mathf.Clamp(transform.position.x, topLeft.position.x, bottomRight.position.x), 
		                                  transform.position.y, 
		                                  Mathf.Clamp(transform.position.z, bottomRight.position.z, topLeft.position.z)
		                                  );
	}

    public void SetPosition(Transform pPosition)
    {
        transform.position = pPosition.position;
    }

//    public void MoveToPostion(Transform pPosition, float pSpeed)
//    {
//
//    }
}
