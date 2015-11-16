using System;
using UnityEngine;

public class TooltipBehaviour : MonoBehaviour
{
	[SerializeField] Renderer highlighting;

	Color startingColor;

	float targetAlpha;

	[SerializeField] float easing = 0.6f;

	void Start()
	{
		startingColor = highlighting.material.color;
		Hide ();
	}

	void Update() 
	{
		highlighting.material.color += new Color (0,0,0,(targetAlpha-highlighting.material.color.a)*easing);
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

