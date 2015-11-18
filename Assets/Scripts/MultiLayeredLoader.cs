using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;
using System.IO;



public class MultiLayeredLoader : MonoBehaviour {

    [SerializeField]
    public UnityEngine.Object xmlTxt;
   

    [SerializeField]
    public GameObject[] GOarray;
    
    int WIDTH;
    int HEIGHT;
    int[,] _data;

    void Start()
    {
        LoadFile();
        
    }

        void LoadFile()
    {

        XmlDocument xml = new XmlDocument();
        xml.PreserveWhitespace = false;
        
        xml.Load(Application.dataPath + "\\Tiled\\" + xmlTxt.name + ".tmx");
     
        XmlNode dataNode = xml.SelectSingleNode("/map/layer/data");
        XmlNode mapNode = xml.SelectSingleNode("/map");
        XmlNodeList layerNodeList = xml.SelectNodes("/map/layer");

        WIDTH = int.Parse(mapNode.Attributes["width"].Value); //tiled width
        HEIGHT = int.Parse(mapNode.Attributes["height"].Value); // tiled height

        _data = new int[WIDTH, HEIGHT];

        for (int k = 0; k < layerNodeList.Count; k++) {
            XmlNode layerNode = layerNodeList[k];
            Debug.Log("Innertext of layers: " + layerNode.InnerText);

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
                    
                    _data[j - 1, i] = temp;
                    
                    if (temp != 0)
                    Instantiate(GOarray[temp], new Vector3(WIDTH + i, k, HEIGHT + j ), Quaternion.identity);
                    
                }
            }

        }
        
    }

   

}