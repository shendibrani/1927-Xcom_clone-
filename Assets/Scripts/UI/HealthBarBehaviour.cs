using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarBehaviour : MonoBehaviour {

    [SerializeField] Image bar;
    [SerializeField] Health linkedHealthComponent;
	
	float ratio { get { return (float)linkedHealthComponent.health/(float)linkedHealthComponent.maxHealth; } }

	void Start()
	{
		bar.fillAmount = ratio;
	}

	void Update () 
	{
		if(linkedHealthComponent.maxHealth != 0){
			bar.fillAmount += (ratio-bar.fillAmount)*0.2f;
			//Debug.Log(ratio);
		}
	}
}
