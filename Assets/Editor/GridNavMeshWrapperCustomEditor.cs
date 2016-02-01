using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(GridNavMeshWrapper))]

public class GridNavMeshWrapperCustomEditor : Editor {

	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Set Starting Node")){
            //RaycastHit[] hit = Physics.RaycastAll((target as GridNavMeshWrapper).transform.position, Vector3.down,3f);
            RaycastHit[] hit = Physics.SphereCastAll((target as GridNavMeshWrapper).transform.position, 0.1f, Vector3.down,3f);
            //Physics.RaycastAll();
            foreach (RaycastHit r in hit){
                if(r.transform.GetComponent<NodeBehaviour>() != null){
					(target as GridNavMeshWrapper).StartingNode = r.collider.GetComponent<NodeBehaviour>();
					(target as GridNavMeshWrapper).position = (target as GridNavMeshWrapper).StartingNode.offsetPosition;
					EditorUtility.SetDirty(target);
                    break;
				}
            }
			/*if(Physics.RaycastAll((target as GridNavMeshWrapper).transform.position, Vector3.down, out hit)){
				if(hit.collider.GetComponent<NodeBehaviour>() != null){
					(target as GridNavMeshWrapper).StartingNode = hit.collider.GetComponent<NodeBehaviour>();
					(target as GridNavMeshWrapper).position = (target as GridNavMeshWrapper).StartingNode.offsetPosition;
					EditorUtility.SetDirty(target);
				}
			}*/
		}

		if(GUILayout.Button("Set All Starting Nodes"){
			foreach(GridNavMeshWrapper gnmw in GameObject.FindObjectsOfType<GridNavMeshWrapper>()){
				RaycastHit[] hit = Physics.SphereCastAll((gnmw).transform.position, 0.1f, Vector3.down,3f);
				//Physics.RaycastAll();
				foreach (RaycastHit r in hit){
					if(r.transform.GetComponent<NodeBehaviour>() != null){
						(gnmw).StartingNode = r.collider.GetComponent<NodeBehaviour>();
						(gnmw).position = (gnmw).StartingNode.offsetPosition;
						EditorUtility.SetDirty(target);
						break;
					}
				}
			}
		}
	}
}
