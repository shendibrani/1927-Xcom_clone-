using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{

	public delegate void VoidVoidDelegate();

	[SerializeField] bool debug;

	public event VoidVoidDelegate Selected, Deselected;

	public void OnSelect(){
		if(Selected != null) Selected.Invoke ();
	}

	public void OnDeselect(){
		if(Deselected != null) Deselected.Invoke ();
	}
}

