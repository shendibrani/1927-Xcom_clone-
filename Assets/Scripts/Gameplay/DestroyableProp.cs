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
		destroyed.enabled = false;
		GetComponent<Health> ().OnDeath.AddListener(ShowDestroyed);
		currentNode.currentObject = this;
	}

//	void Update(){
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			ShowDestroyed();
//		}
//	}

	void ShowDestroyed()
	{
		intact.enabled = false;
		destroyed.enabled = true;
		currentNode.currentObject = null;
		GetComponent<NavMeshObstacle> ().enabled = false;
	}

	public void OnTargeted(Pawn targeter){}
}

