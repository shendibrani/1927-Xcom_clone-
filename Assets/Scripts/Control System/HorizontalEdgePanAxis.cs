using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HorizontalEdgePanAxis : Axis {
	
	[SerializeField] float tolerance = 25;
	
	// Update is called once per frame
	void Update () 
	{
		_axisValue = 0;
		if(focus){
			if(Input.mousePosition.x <= tolerance && CheckRay()){
				_axisValue = -1;
			} else if(Input.mousePosition.x >= Screen.width - tolerance && CheckRay()){
				_axisValue = 1;
			}
		}
	}

    bool CheckRay()
    {
        // get pointer event data, then set current mouse position
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;

        // create an empty list of raycast results
        List<RaycastResult> hits = new List<RaycastResult>();

        // ray cast into UI and check for hits
        EventSystem.current.RaycastAll(ped, hits);

        // check any hits to see if any of them are blocking UI elements
        foreach (RaycastResult r in hits)
        {
            if (r.gameObject.GetComponent<RectTransform>()) return false;
        }
        return true;
    }
}