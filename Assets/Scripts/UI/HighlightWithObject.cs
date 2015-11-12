using System;
using UnityEngine;

public class HighlightWithObject : Highlightable
{
	[SerializeField] Renderer highlighting;

	Color startingColor;

	protected override void Start()
	{
		startingColor = highlighting.material.color;
		base.Start();
	}

	public override void ProcessHighlight ()
	{
		if(highlighted){
			highlighting.material.color = startingColor;
		}
		else {
			highlighting.material.color = Color.clear;
		}
	}
}

