using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(LoadXML))]

public class LoadXML_Editor : Editor {
	
	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector();
		if(GUILayout.Button("Build")){
			(target as LoadXML).DestroyAllLoaded();
            (target as LoadXML).runOnce = false;
            (target as LoadXML).LoadTo3D();
            
            foreach (NodeBehaviour node in FindObjectsOfType<NodeBehaviour>()){
				node.NodeSetup();
				EditorUtility.SetDirty(node);
			}
			
			foreach(GridMovementBehaviour gmb in FindObjectsOfType<GridMovementBehaviour>()){
				gmb.position = gmb.StartingNode.offsetPosition;
			}
		}

        if (GUILayout.Button("Delete Generated"))
        {
            (target as LoadXML).DestroyAllLoaded();
        }
        

    }

    
}