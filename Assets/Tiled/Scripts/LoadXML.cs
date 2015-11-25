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
    
    const uint FLIPPED_HORIZONTALLY_FLAG = 0x80000000;
    const uint FLIPPED_VERTICALLY_FLAG = 0x40000000;
    const uint FLIPPED_DIAGONALLY_FLAG = 0x20000000;
    const uint FLIP_MASK = ~(FLIPPED_HORIZONTALLY_FLAG | FLIPPED_VERTICALLY_FLAG | FLIPPED_DIAGONALLY_FLAG);
    
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

                        instanceLocation = new Vector3( i * X_MarginDistance, k * Y_MarginDistance, j * -Z_MarginDistance);
                        var tile = data[j, i, k];
                       
                        bool flippedHorizontal;
                        bool flippedVertical;
                        bool flippedDiagonal;

                        int tileID = GetTiledID(tile, out flippedHorizontal, out flippedVertical, out flippedDiagonal);

                        if (tileID != 0)
                        {
                            Quaternion rotation = Quaternion.identity;
                            //if (flippedHorizontal) rotation = Quaternion.Euler(0, 180, 0);
                            if (flippedVertical & !flippedDiagonal) rotation = Quaternion.Euler(0, 90, 0);
                            else if (flippedDiagonal & !flippedVertical) rotation = Quaternion.Euler(0, 180, 0);
                            else if (flippedDiagonal & flippedVertical) rotation = Quaternion.Euler(0, 270, 0);

                            Debug.Log("Tile " + tileID);
                            tempStore = (GameObject)Instantiate(PrefabLoader[tileID], instanceLocation, rotation);
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

    public static int GetTiledID(uint Data, out bool flippedH, out bool flippedD, out bool flippedV) {
        bool FlippedHorizontally = (Data & FLIPPED_HORIZONTALLY_FLAG) > 0;
        bool FlippedVertically = (Data & FLIPPED_VERTICALLY_FLAG) > 0;
        bool FlippedDiagonally = (Data & FLIPPED_DIAGONALLY_FLAG) > 0;

        flippedH = FlippedHorizontally;
        flippedD = FlippedDiagonally;
        flippedV = FlippedVertically;

        return (int)(Data & FLIP_MASK);
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

