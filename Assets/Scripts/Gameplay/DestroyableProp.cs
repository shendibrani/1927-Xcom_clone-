using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshObstacle))]
public class DestroyableProp : MonoBehaviour, Targetable
{
	[SerializeField] Renderer intact, destroyed;
	[SerializeField] NodeBehaviour currentNode;

	void Start ()
	{
		destroyed.material.color -= new Color (0, 0, 0, 1); 
		GetComponent<Health> ().OnDeath.AddListener(ShowDestroyed);
		currentNode.currentObject = this;
	}

	void ShowDestroyed()
	{
		intact.material.color -= new Color (0, 0, 0, 1); 
		destroyed.material.color += new Color (0, 0, 0, 1);
		currentNode.currentObject = null;
		GetComponent<NavMeshObstacle> ().enabled = false;
	}

	public void OnTargeted(Pawn targeter){}
}

