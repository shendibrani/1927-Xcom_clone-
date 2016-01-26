using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(RectTransform))]
public class UIDrawerBehaviour : MonoBehaviour {

	[SerializeField] Vector2 visiblePosition;
	[SerializeField] Vector2 hiddenPosition;

	[SerializeField] float easing = 0.2f;

	public bool visible {get; private set;}

	// Update is called once per frame
	void Update () 
	{
		RectTransform rectTransform = GetComponent<RectTransform> ();
		Vector2 positionTarget = visible ? visiblePosition : hiddenPosition;
		rectTransform.anchoredPosition += (positionTarget - rectTransform.anchoredPosition)*easing;
	}

	public void Show(){
		visible = true;
	}

	public void Hide(){
		visible = false;
	}
}
