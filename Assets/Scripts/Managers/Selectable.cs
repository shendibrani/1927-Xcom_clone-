using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Selectable : MonoBehaviour
{
	[SerializeField] bool debug;

	public UnityEvent Selected, Deselected;

	public void OnSelect(){
		if(Selected != null) Selected.Invoke ();
	}

	public void OnDeselect(){
		if(Deselected != null) Deselected.Invoke ();
	}
}

