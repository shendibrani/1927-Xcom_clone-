using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(NodeBehaviour))]

public class NodeBehaviourLinkBuilder : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		if(GUILayout.Button("Refresh Links")){
			foreach(NodeBehaviour node in FindObjectsOfType<NodeBehaviour>()){
				node.NodeSetup();
				EditorUtility.SetDirty(node);
			}

			foreach(GridMovementBehaviour gmb in FindObjectsOfType<GridMovementBehaviour>()){
				gmb.position = gmb.StartingNode.offsetPosition;
			}
		}
	}
}
