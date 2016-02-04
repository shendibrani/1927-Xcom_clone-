using UnityEngine;
using System.Collections;

public class PickupCollectable : MonoBehaviour {

    [SerializeField]
    NodeBehaviour attachedNode;

	// Use this for initialization
	void Start () {
        if (attachedNode == null)
            RaycastToStartingNode();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //transform.position = attachedNode.offsetPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (attachedNode.currentObject != null && attachedNode.currentObject.GetComponent<Pawn>())
        {
            attachedNode.currentObject.GetComponent<Pawn>().LevelUp();
            attachedNode = null;
            DestroyObject(this);
        }
	}

    void RaycastToStartingNode()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, 0.1f, Vector3.down, 3f);

        foreach (RaycastHit r in hit)
        {
            if (r.transform.GetComponent<NodeBehaviour>() != null)
            {
                attachedNode = r.collider.GetComponent<NodeBehaviour>();
                transform.position = attachedNode.offsetPosition;
                break;
            }
        }
    }
}
