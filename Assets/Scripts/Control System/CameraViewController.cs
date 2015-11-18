using UnityEngine;
using System.Collections;

[RequireComponent(typeof(VerticalEdgePanAxis))]
[RequireComponent(typeof(HorizontalEdgePanAxis))]
[RequireComponent(typeof(MouseRotateAxis))]
public class CameraViewController : MonoBehaviour {

    VerticalEdgePanAxis verticalAxis;
    HorizontalEdgePanAxis horizontalAxis;
	MouseRotateAxis rotateAxis;

	[SerializeField] float panSpeed = 0.5f;
	[SerializeField] float rotateSpeed = 36f;

	// Use this for initialization
	void Start () {
        verticalAxis = GetComponent<VerticalEdgePanAxis>();
        horizontalAxis = GetComponent<HorizontalEdgePanAxis>();
		rotateAxis = GetComponent<MouseRotateAxis> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(2))
        {
            RotateCamera();
        }
        else
        {
            SlideCamera();
        }
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

    public void RotateCamera(Vector3 pRotation)
    {
        transform.Rotate(pRotation);
    }

    public void SetPosition(Transform pPosition)
    {
        transform.position = pPosition.position;
    }

    public void MoveToPostion(Transform pPosition, float pSpeed)
    {

    }
}
