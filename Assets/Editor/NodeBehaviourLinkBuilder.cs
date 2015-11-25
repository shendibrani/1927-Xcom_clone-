using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(NodeBehaviour))]

public class NodeBehaviourLinkBuilder : Editor {

	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Refresh Links")){
			foreach(NodeBehaviour node in FindObjectsOfType<NodeBehaviour>()){
				node.NodeSetup();
				EditorUtility.SetDirty(node);
			}

			foreach(GridNavMeshWrapper wrapper in FindObjectsOfType<GridNavMeshWrapper>()){
				if(wrapper.StartingNode != null){
					wrapper.position = wrapper.StartingNode.offsetPosition;
				}
			}
		}
	}
}
