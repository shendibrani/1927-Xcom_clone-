using UnityEngine;
using System.Collections;

public class HighlightMaterial : Highlightable 
{
	[SerializeField] Material material;
	[SerializeField] Color startingColor;
	[SerializeField] Color highlightColor;

	// Use this for initialization
	protected override void Start () 
	{
		material = GetComponent<Renderer>().material;
		startingColor = material.color;
		base.Start();
	}

	public override void ProcessHighlight ()
	{
		if(highlighted){
			material.color = highlightColor;
		}
		else {
			material.color = startingColor;
		}
	}
}
