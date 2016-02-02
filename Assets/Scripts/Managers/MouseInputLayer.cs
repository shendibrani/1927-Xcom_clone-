using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseInputLayer : MonoBehaviour 
{
    //checks if ray does not hit a UI Element (RectTransform), returns true when no UI;
	bool CheckRay()
	{
		// get pointer event data, then set current mouse position
		//PointerEventData ped = new PointerEventData(EventSystem.current);
		//ped.position = Input.mousePosition;
		
		
		// create an empty list of raycast results
		//List<RaycastHit> hits = new List<RaycastHit>();
		RaycastHit[] hits;
		
		// ray cast into UI and check for hits
		//EventSystem.current.RaycastAll(ped, hits);
		hits = Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition), 100, Physics.IgnoreRaycastLayer);
		
		// check any hits to see if any of them are blocking UI elements
		if (hits != null)
		{
			foreach (RaycastHit r in hits)
			{
				if (r.collider.gameObject != null && r.collider.gameObject.GetComponent<RectTransform>()) return false;
			}
		}
		return true;
	}

    // Update is called once per frame
    void Update()
    {
        if (CheckRay())
        {
            if (!TurnManager.instance.busy)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (SelectionManager.instance.isInTargetingMode)
                        {
                            if (hit.collider.GetComponent<Targetable>() != null)
                            {
                                SelectionManager.target = hit.collider.GetComponent<Targetable>();
                            }
                        }
                        else
                        {
                            if (hit.collider.GetComponent<Selectable>() != null)
                            {
                                SelectionManager.selected = hit.collider.GetComponent<Selectable>();
                            }
                        }
                    }
                }

                if (Input.GetMouseButtonUp(1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (SelectionManager.instance.isInTargetingMode)
                        {
                            if (hit.collider.GetComponent<Targetable>() != null)
                            {
                                SelectionManager.target = hit.collider.GetComponent<Targetable>();
                                SelectionManager.Execute();
                            }
                        }
                        else
                        {
                            if (SelectionManager.selected != null &&
                                SelectionManager.selected.GetComponent<Pawn>() != null &&
                                hit.collider.GetComponent<NodeBehaviour>() != null)
                            {
                                SelectionManager.command = Factory.GetCommand(Commands.Move, SelectionManager.selected.GetComponent<Pawn>());
                                SelectionManager.command.target = hit.collider.GetComponent<Targetable>();
                                if (SelectionManager.Execute())
                                {
                                    TurnManager.instance.SetBusy();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
