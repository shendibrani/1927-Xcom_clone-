using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tipSkillTool : MonoBehaviour {

    [SerializeField]
    Text nameText;
    [SerializeField]
    Text desciprtionText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        

    }

    public void Populate(Skill s)
    {
        nameText.text = s.name;
        desciprtionText.text = s.description;
    }
}
