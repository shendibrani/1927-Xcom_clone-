using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;
using System.IO;


public class MultiLayeredLoader : MonoBehaviour {
    [NonSerialized]
    public bool runOnce = false;

    [SerializeField]
    public UnityEngine.Object TiledSaveFile;
    public ushort X_MarginDistance = 1;
    public ushort Y_MarginDistance = 1;
    public ushort Z_MarginDistance = 1;

    private Vector3 instanceLocation;
    public GameObject ParentOfInstantiations;

    public bool printLayersInnerInfo;
   
    [SerializeField]
    [Tooltip("Enter the size of IDs of your Tiled save file into the Prefab Loader. Then Load the prefabs you want to replace your tiled ID's with. Element 0 is empty space, therefore always empty. Note: When creating a new file in Tiled, select CSV for the tile layer format")]
    public GameObject[] PrefabLoader;

    GameObject tempStore;

    Vector3 originalPosition;
    int WIDTH;
    int HEIGHT;
    int[,] _data;

    void Start()
    {
    
        
        if (PrefabLoader[0] != null) {
            Debug.LogError("You were trying to initialize a GameObject in Element 0, you shouldn't do that. bye...");
            PrefabLoader[0] = null;
        }
    }

    void Update()
    {
        
        if (X_MarginDistance != originalPosition.x || Y_MarginDistance != originalPosition.y || Z_MarginDistance != originalPosition.z)
        {
            DestroyAllLoaded();
            runOnce = false;
            
        }
        LoadFile();
    }

     public void LoadFile()
     {
        if (!runOnce) { 
        XmlDocument xml = new XmlDocument();
        xml.PreserveWhitespace = false;
        
        xml.Load(Application.dataPath + "\\Tiled\\" + TiledSaveFile.name + ".tmx");
  
        XmlNode mapNode = xml.SelectSingleNode("/map");
        XmlNodeList layerNodeList = xml.SelectNodes("/map/layer");

        WIDTH = int.Parse(mapNode.Attributes["width"].Value); //tiled width
        HEIGHT = int.Parse(mapNode.Attributes["height"].Value); // tiled height

        _data = new int[WIDTH, HEIGHT];

        for (int k = 0; k < layerNodeList.Count; k++) {
            XmlNode layerNode = layerNodeList[k];
            
            if (printLayersInnerInfo) Debug.Log("Layer " + (k + 1) + " out of ["+ layerNodeList.Count + "] Contains: " + layerNode.InnerText);

            string[] splitLines = layerNode.InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 1; j <= HEIGHT; j++)
            {

                string[] cols = splitLines[j].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    
                for (int i = 0; i < WIDTH; i++)
                {
                    
                    string col = cols[i];
                    int temp;
                    if (!int.TryParse(col, out temp))
                    {
                        Debug.Log(col + i + j);
                    }
                      
                    if (PrefabLoader[temp] != null)
                    {
                            instanceLocation = new Vector3(WIDTH + i * X_MarginDistance, k * Y_MarginDistance, HEIGHT + j * Z_MarginDistance);
                            originalPosition = new Vector3(X_MarginDistance, Y_MarginDistance, Z_MarginDistance);
                            tempStore = (GameObject)Instantiate(PrefabLoader[temp], instanceLocation, Quaternion.identity);
                            
                            if ( ParentOfInstantiations != null) tempStore.transform.parent = ParentOfInstantiations.transform;
                            tempStore.tag = "CustomGenerated";
                        } else if (PrefabLoader[temp] == null && tempStore == null)
                        {
                            Debug.LogError("You have run into a weird bug, for now just increment the size of the prefab loader by 1.");
                        }
                     runOnce = true;
                    }
            }

        }
        
        }
    }

    public void DestroyAllLoaded() {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("CustomGenerated")) {
            DestroyImmediate(go);                                                                                                                                          
        }
    }

}