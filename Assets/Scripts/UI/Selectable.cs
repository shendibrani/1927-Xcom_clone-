using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{

	public delegate void VoidVoidDelegate();

	[SerializeField] bool debug;

	public event VoidVoidDelegate Selected, Deselected;

	void OnMouseEnter()
	{
		if(debug){Debug.Log("Hovered");}
		SelectionManager.hovered = this;
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonUp(0)){
			if(debug){Debug.Log("Select");}
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

