using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Targetable))]
public class DestroyableProp : MonoBehaviour
{
	Renderer intact; //destroyed;
	[SerializeField] NodeBehaviour currentNode;

	void Start ()
	{
		//destroyed.enabled = false;
		intact = GetComponent<Renderer> ();
		GetComponent<Health> ().OnDeath.AddListener(ShowDestroyed);
        if (currentNode != null)
            currentNode.currentObject = GetComponent<Targetable>();
        else Debug.Log("Destroyable Prop " + gameObject + " is not attached to node");
	}

//	void Update(){
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			ShowDestroyed();
//		}
//	}
    public void DamageProp()
    {
        ShowDestroyed(null);
    }

	void ShowDestroyed(Pawn p)
	{
		intact.enabled = false;
		//destroyed.enabled = true;
		currentNode.currentObject = null;
		GetComponent<NavMeshObstacle> ().enabled = false;
	}
}

