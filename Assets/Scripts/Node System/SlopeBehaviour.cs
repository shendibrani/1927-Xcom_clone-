using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlopeBehaviour : NodeBehaviour
{
    public override void NodeSetup()
    {
        links = new List<NodeBehaviour>();
        RaycastHit hit = new RaycastHit();

		if (Physics.Raycast(position, transform.forward, out hit,1))
        {
            if (hit.collider.gameObject.GetComponent<NodeBehaviour>() != null)
            {
                Bind(hit.collider.gameObject.GetComponent<NodeBehaviour>());
            }
        }

		if (Physics.Raycast(position, ((transform.forward*-1) + (transform.up*-1)).normalized , out hit,1))
        {
            if (hit.collider.gameObject.GetComponent<NodeBehaviour>() != null)
            {
                Bind(hit.collider.gameObject.GetComponent<NodeBehaviour>());
            }
        }

		if (Physics.Raycast (position, (transform.forward + Vector3.up).normalized, out hit,1)) {
			if (hit.collider.gameObject.GetComponent<SlopeBehaviour> () != null) {
				Bind (hit.collider.gameObject.GetComponent<SlopeBehaviour> ());
			}
		}
    }
}
