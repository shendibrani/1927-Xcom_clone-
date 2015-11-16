using UnityEngine;
using System.Collections;

[RequireComponent(typeof(VerticalEdgePanAxis))]
[RequireComponent(typeof(HorizontalEdgePanAxis))]
public class CameraViewController : MonoBehaviour {

    VerticalEdgePanAxis verticalAxis;
    HorizontalEdgePanAxis horizontalAxis;

	// Use this for initialization
	void Start () {
        verticalAxis = GetComponent<VerticalEdgePanAxis>();
        horizontalAxis = GetComponent<HorizontalEdgePanAxis>();
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
        transform.position += transform.right * horizontalAxis.axisValue;
        transform.position += transform.forward * verticalAxis.axisValue;
    }

    void RotateCamera()
    {
        transform.Rotate(0, horizontalAxis.axisValue, 0);
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
