using UnityEngine;
using System.Xml;
using System;

public class ReadXML : MonoBehaviour {
    [SerializeField]
    public UnityEngine.Object TiledSaveFile;
    public bool printLayersInnerInfo;

    [NonSerialized]
    public int HEIGHT, WIDTH, DEPTH;
    public uint[,,] data;
    
    public void ReadXMLFile() {

        XmlDocument xml = new XmlDocument();
        xml.PreserveWhitespace = false;
        xml.Load(Application.dataPath + " \\Tiled\\" + TiledSaveFile.name + ".tmx");

        XmlNode mapNode = xml.SelectSingleNode("/map");
        XmlNodeList layerNodeList = xml.SelectNodes("/map/layer");

        WIDTH = int.Parse(mapNode.Attributes["width"].Value);
        HEIGHT = int.Parse(mapNode.Attributes["height"].Value);
        DEPTH = layerNodeList.Count;

        data = new uint[HEIGHT, WIDTH, DEPTH];

        for (int k = 0; k < DEPTH; k++)
        {
            XmlNode layerNode = layerNodeList[k];

            string[] splitLines = layerNode.InnerText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (printLayersInnerInfo) Debug.Log("Layer " + (k + 1) + " out of [" + layerNodeList.Count + "] Contains: " + layerNode.InnerText);

            for (int j = 1; j <= HEIGHT; j++)
            { 
                string[] cols = splitLines[j].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < WIDTH; i++)
                {

                    string col = cols[i];
                    //int temp = int.Parse(col);
                    data[j - 1, i, k] = uint.Parse(col);
                    
                }
            }

        }
    } 
}
