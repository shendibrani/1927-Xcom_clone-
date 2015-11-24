using UnityEngine;
using System.Collections.Generic;
using System;

public class LoadXML : ReadXML {
    public int X_MarginDistance = 1;
    public int Y_MarginDistance = 1;
    public int Z_MarginDistance = 1;

    private Vector3 instanceLocation;
    public GameObject ParentOfInstantiations;
    public GameObject defaultInstantiation;

    public bool generateMesh;
    // Use this for initialization
    public List<GameObject> PrefabLoader;
    private List<GameObject> generatedObjects = new List<GameObject>();

    GameObject tempStore;
    GameObject layerParent;

    [NonSerialized]
    public bool runOnce = true;

    void Update() {
        LoadTo3D();
    }
    
    public void LoadTo3D()
    {
        if (!runOnce)
        {
            ReadXMLFile();
            generatedObjects.Clear();
            int counter = 1;
            for (int k = 0; k < DEPTH; k++)
            {
                layerParent = new GameObject();
                layerParent.tag = "CustomGenerated";
                layerParent.name = "ParentOfLayer" + counter;
                counter++;

                if (generateMesh) layerParent.AddComponent<CombineChildren>();
                
                for (int j = 0; j < HEIGHT; j++)
                {
                    for (int i = 0; i < WIDTH; i++)
                    {

                        instanceLocation = new Vector3( i * X_MarginDistance, k * Y_MarginDistance, j * Z_MarginDistance);
                        int tile = data[j, i, k];

                        if (tile != 0)
                        {
                            tempStore = (GameObject)Instantiate(PrefabLoader[tile], instanceLocation, Quaternion.identity);
                            //tempStore = (GameObject)Resources.Load("//Assets//Tiled//Cube");
                            generatedObjects.Add(tempStore);
                            tempStore.transform.parent = layerParent.transform;
                            tempStore.tag = "CustomGenerated";
                        }
                        runOnce = true;
                    }
                }
            }

        }
        
    }

    public void DestroyOthers() {
        if (generateMesh & UnityEngine.Application.isPlaying)
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("CustomGenerated")) {
            if (child.name != "Combined mesh") {
                DestroyImmediate(child);
            }
        }
    }

    public void DestroyAllLoaded()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("CustomGenerated"))
        {
            DestroyImmediate(go);
        }
    }
}

