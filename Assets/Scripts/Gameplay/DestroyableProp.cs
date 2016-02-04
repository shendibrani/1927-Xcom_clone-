using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(Targetable))]
public class DestroyableProp : MonoBehaviour
{
	private static List<DestroyableProp> _all;
	
	public static List<DestroyableProp> all {
		get{
			if(_all == null){
				_all = new List<DestroyableProp>(FindObjectsOfType<DestroyableProp>());
			}
			return _all;
		}
	}

	Renderer intact; //destroyed;
	[SerializeField] NodeBehaviour currentNode;

	void Start ()
	{
		//destroyed.enabled = false;
		//intact = GetComponent<Renderer> ();
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

	public void ShowDestroyed(Pawn p)
	{
		//intact.enabled = false;
		//destroyed.enabled = true;
		currentNode.currentObject = null;
		GetComponent<NavMeshObstacle> ().enabled = false;
		Destroy (this.gameObject);
	}

	#region Callbacks
	
	void OnDestroy(){
		if(_all != null)
			_all.Remove(this);
	}
	
	#endregion
}

