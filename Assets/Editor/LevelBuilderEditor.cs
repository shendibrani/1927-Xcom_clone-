using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(MultiLayeredLoader))]

public class LevelBuilderEditor : Editor {
	
	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Build")){
			(target as MultiLayeredLoader).DestroyAllLoaded();
			(target as MultiLayeredLoader).LoadFile();
            (target as MultiLayeredLoader).runOnce = false;
            foreach (NodeBehaviour node in FindObjectsOfType<NodeBehaviour>()){
				node.NodeSetup();
				EditorUtility.SetDirty(node);
			}
			
			foreach(GridMovementBehaviour gmb in FindObjectsOfType<GridMovementBehaviour>()){
				gmb.position = gmb.StartingNode.offsetPosition;
			}
		}
	}
}