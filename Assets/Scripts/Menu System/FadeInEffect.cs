using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class FadeInEffect : MonoBehaviour
{
	[SerializeField] float fadeinSpeed = 0.2f;
	[SerializeField] UnityEvent FadeComplete;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Image>().color -= new Color (0,0,0,1);
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Image>().color += new Color(0,0,0,Time.deltaTime * fadeinSpeed);
		if(GetComponent<Image>().color.a >=1){
			FadeComplete.Invoke();
			enabled = false;
		}
	}
}

