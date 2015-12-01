using UnityEngine;
using System.Collections;

public class MouseInputLayer : MonoBehaviour 
{

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonUp (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

			if(Physics.Raycast(ray,out hit))
			{
				if(SelectionManager.instance.isInTargetingMode)
				{
					if(hit.collider.GetComponent<Targetable>() != null)
					{
						SelectionManager.target = hit.collider.GetComponent<Targetable>();
					}
				} 
				else 
				{
					if(hit.collider.GetComponent<Selectable>() != null)
					{
						SelectionManager.selected = hit.collider.GetComponent<Selectable>();
					}
				}
			}
		}
	}
}
