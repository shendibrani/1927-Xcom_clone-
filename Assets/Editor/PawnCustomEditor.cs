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
			RaycastHit hit;
			if(Physics.Raycast((target as GridNavMeshWrapper).transform.position, Vector3.down, hit)){
				if(hit.collider.GetComponent<NodeBehaviour>() != null){
					(target as GridNavMeshWrapper).StartingNode = hit.collider.GetComponent<NodeBehaviour>();
					(target as GridNavMeshWrapper).position = (target as GridNavMeshWrapper).StartingNode.offsetPosition;
				}
			}
		}
	}
}
