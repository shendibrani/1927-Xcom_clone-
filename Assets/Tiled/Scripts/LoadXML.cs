using UnityEngine;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class LoadXML : ReadXML {
    public int X_MarginDistance = 1;
    public int Y_MarginDistance = 1;
    public int Z_MarginDistance = 1;

    private Vector3 instanceLocation;
    public GameObject defaultInstantiation;

    const uint FLIPPED_HORIZONTALLY_FLAG = 0x80000000;
    const uint FLIPPED_VERTICALLY_FLAG = 0x40000000;
    const uint FLIPPED_DIAGONALLY_FLAG = 0x20000000;
    const uint FLIP_MASK = ~(FLIPPED_HORIZONTALLY_FLAG | FLIPPED_VERTICALLY_FLAG | FLIPPED_DIAGONALLY_FLAG);

    bool flippedHorizontal;
    bool flippedVertical;
    bool flippedDiagonal;

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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ReadXMLFile();
            generatedObjects.Clear();
            int counter = 1;

            for (int k = 0; k < DEPTH; k++)
            {
                layerParent = new GameObject();
                generatedObjects.Add(layerParent);
                layerParent.name = "ParentOfLayer" + counter;
                counter++;

                for (int j = 0; j < HEIGHT; j++)
                {
                    for (int i = 0; i < WIDTH; i++)
                    {

                        instanceLocation = new Vector3( i * X_MarginDistance, k * Y_MarginDistance, j * -Z_MarginDistance);
                        uint tile = data[j, i, k];
                        
                        int tileID = GetTiledID(tile, out flippedHorizontal, out flippedVertical, out flippedDiagonal);
                        Quaternion rotation = rotationSet();

                        if (tileID != 0)
                        {
							if (PrefabLoader[tileID].gameObject.tag == "randomizer")
							{
								tempStore = (GameObject)Instantiate(PrefabLoader[tileID], instanceLocation, rotation); 

								int _i = UnityEngine.Random.Range(0,4);
								tempStore.transform.Rotate(0,_i * 90,0);

								GameObject toDelete = tempStore;

								tempStore = tempStore.GetComponent<RandomLoad>().Generate();
								tempStore.transform.parent = layerParent.transform;

								DestroyImmediate (toDelete.gameObject);
								generatedObjects.Add(tempStore);
                            }
							else
							{
								tempStore = (GameObject)Instantiate(PrefabLoader[tileID], instanceLocation, rotation);
								generatedObjects.Add(tempStore);
								tempStore.transform.parent = layerParent.transform;
							}
                        }
						
				 		runOnce = true;
                    }
                }
            }
          
            sw.Stop();
            UnityEngine.Debug.Log("Elapsed time to generate structure: " + sw.ElapsedMilliseconds + "ms" + "||" + "Amount of generated objects: " + generatedObjects.Count);
        }
        
    }

    private Quaternion rotationSet() {
        Quaternion rotation = Quaternion.identity;

        if (flippedVertical & !flippedDiagonal) rotation = Quaternion.Euler(0, 90, 0);
        else if (flippedDiagonal & !flippedVertical) rotation = Quaternion.Euler(0, 180, 0);
        else if (flippedDiagonal & flippedVertical) rotation = Quaternion.Euler(0, 270, 0);

        return rotation;
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

    public void DestroyAllLoaded()
    {
        foreach (GameObject go in generatedObjects) {
            DestroyImmediate(go);
        }
    }
}

