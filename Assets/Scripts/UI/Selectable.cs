using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{

	public delegate void VoidVoidDelegate();
	
	public event VoidVoidDelegate Selected, Deselected;

	void OnMouseEnter()
	{
		SelectionManager.hovered = this;
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp(0)){
			//Debug.Log ("Select");
			SelectionManager.selected = this;
		}
	}

	public void OnSelect(){
		if(Selected != null) Selected.Invoke ();
	}

	public void OnDeselect(){
		if(Deselected != null) Deselected.Invoke ();
	}
}

