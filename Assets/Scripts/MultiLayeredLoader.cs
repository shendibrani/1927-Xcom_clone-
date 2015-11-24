using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;
using System.IO;
using System.Linq;

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
    public GameObject defaultInstantiation;

    public bool printLayersInnerInfo;
   
    [SerializeField]
    [Tooltip("Enter the size of IDs of your Tiled save file into the Prefab Loader. Then Load the prefabs you want to replace your tiled ID's with. Element 0 is empty space, therefore always empty. Note: When creating a new file in Tiled, select CSV for the tile layer format")]
   // public GameObject[] PrefabLoader;
    public List<GameObject> PrefabLoader;
    GameObject tempStore;

    Vector3 originalPosition;
    int WIDTH;
    int HEIGHT;
    int[,] _data;

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
            
            GameObject layerParent;

            for (int k = 0; k < layerNodeList.Count; k++) {
                
                layerParent = (GameObject)Instantiate(ParentOfInstantiations);
                layerParent.tag = "CustomGenerated";
                XmlNode layerNode = layerNodeList[k];

                Debug.Log("LayerNode Max before the if statement.." + layerNode.InnerText.Max());

                if (PrefabLoader.Count < layerNode.InnerText.Max())
                {
                    Debug.Log ("We're in the magical if statement: ");
                   // PrefabLoader.Add(defaultInstantiation);
                    Debug.Log("Max Int in layer[" + (k + 1) + "]: " + layerNode.InnerText.Max());
                    Debug.Log("PrefabLoader Count: " + PrefabLoader.Count);
                    Debug.LogError ("This message is in an if statement that has been set to evaluate if the size of the PrefabLoader is bigger than the Maximum Integer in each Layer..\n"+
                                " Like most, by now you have probably realized that that's basically comparing if the value 5 is lower than 2. That's because I set my prefab Loader to 5 \n"
                               + " , and the biggest integer in my save file is 2... As you can see by the previous debug logs... It's day 1 of the world's end... every man must fend for his own logic!");
                }
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

                        if (temp != 0 & PrefabLoader[temp] == null & defaultInstantiation != null) PrefabLoader[temp] = defaultInstantiation;
                        if (temp != 0 & PrefabLoader[temp] != null )
                        {

                            instanceLocation = new Vector3(WIDTH + i * X_MarginDistance, k * Y_MarginDistance, HEIGHT + j * Z_MarginDistance);
                            originalPosition = new Vector3(X_MarginDistance, Y_MarginDistance, Z_MarginDistance);
                            EmptyElement0();
                           
                            tempStore = (GameObject)Instantiate(PrefabLoader[temp], instanceLocation, Quaternion.identity);
                            tempStore.transform.parent = layerParent.transform;
                            tempStore.tag = "CustomGenerated";
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

    void EmptyElement0() {
        if (PrefabLoader[0] != null)
        {
            Debug.LogError("You were trying to initialize a GameObject in Element 0, you shouldn't do that. bye...");
            PrefabLoader[0] = null ;
        }
    }
}