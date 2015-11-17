using System;
using UnityEngine;

public class TooltipBehaviour : MonoBehaviour
{
	[SerializeField] Renderer tooltip;

	Color startingColor;

	float targetAlpha;

	[SerializeField] float easing = 0.6f;

	void Start()
	{
		startingColor = tooltip.material.color;
		Hide ();
	}

	void Update() 
	{
		foreach(Renderer r in tooltip.GetComponentsInChildren<Renderer>()){
			r.material.color += new Color (0,0,0,(targetAlpha-r.material.color.a)*easing);
		}
	}

	void OnMouseEnter()
	{
		Show ();
	}
	
	void OnMouseExit()
	{
		Hide ();
	}

	void Show()
	{
		targetAlpha = 1;
	}

	void Hide()
	{
		targetAlpha = 0;
	}
}

