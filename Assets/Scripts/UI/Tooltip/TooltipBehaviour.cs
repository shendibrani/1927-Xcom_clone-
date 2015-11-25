using System;
using UnityEngine.UI;
using UnityEngine;

public class TooltipBehaviour : MonoBehaviour
{
	[SerializeField] Canvas tooltip;

	float targetAlpha;

	[SerializeField] float easing = 0.6f;

	void Start()
	{
		Hide ();
	}

	void Update() 
	{
		foreach(Image r in tooltip.GetComponentsInChildren<Image>())
        {
			r.color += new Color (0,0,0,(targetAlpha-r.color.a)*easing);
		}
		foreach(Text r in tooltip.GetComponentsInChildren<Text>())
		{
			r.color += new Color (0,0,0,(targetAlpha-r.color.a)*easing);
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

