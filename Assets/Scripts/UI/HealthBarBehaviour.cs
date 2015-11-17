using UnityEngine;
using System.Collections;

public class HealthBarBehaviour : MonoBehaviour {
	
	[SerializeField] SpriteRenderer bar;
	[SerializeField] Health linkedHealthComponent;
	
	float ratio { get { return (float)linkedHealthComponent.health/(float)linkedHealthComponent.maxHealth; } }

	void Start()
	{
		bar.transform.localScale = new Vector3(ratio, 1, 1);
	}

	void Update () 
	{
		if(linkedHealthComponent.maxHealth != 0){
			bar.transform.localScale = new Vector3(ratio, 0, 0);
			Debug.Log(ratio);
		}
	}
}
