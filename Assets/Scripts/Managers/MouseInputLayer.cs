using UnityEngine;
using System.Collections;

public class MouseInputLayer : MonoBehaviour 
{

	// Update is called once per frame
	void Update () 
	{
		if (!TurnManager.instance.busy) {
			if (Input.GetMouseButtonUp (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();

				if (Physics.Raycast (ray, out hit)) {
					if (SelectionManager.instance.isInTargetingMode) {
						if (hit.collider.GetComponent<Targetable> () != null) {
							SelectionManager.target = hit.collider.GetComponent<Targetable> ();
						}
					} else {
						if (hit.collider.GetComponent<Selectable> () != null) {
							SelectionManager.selected = hit.collider.GetComponent<Selectable> ();
						}
					}
				}
			}

			if (Input.GetMouseButtonUp (1)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				
				if (Physics.Raycast (ray, out hit)) {
					if (SelectionManager.instance.isInTargetingMode) {
						if (hit.collider.GetComponent<Targetable> () != null) {
							SelectionManager.target = hit.collider.GetComponent<Targetable> ();
							SelectionManager.Execute ();
						}
					} else {
						if (SelectionManager.selected != null && 
							SelectionManager.selected.GetComponent<Pawn> () != null &&
							hit.collider.GetComponent<NodeBehaviour> () != null) {
							SelectionManager.command = Factory.GetCommand (Commands.Move, SelectionManager.selected.GetComponent<Pawn> ());
							SelectionManager.command.target = hit.collider.GetComponent<Targetable> ();
							if(SelectionManager.Execute ()){
								TurnManager.instance.SetBusy();
							}
						}
					}
				}
			}
		}
	}
}
