using UnityEngine;
using System.Collections;

public class ExecuteButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnUse()
    {
        if (SelectionManager.command != null)
        {
            SelectionManager.Execute();
        }
    }
}
