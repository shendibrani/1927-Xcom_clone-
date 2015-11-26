using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[SerializeField]
public class Skill : MonoBehaviour{

    Command abilityCommand; //replace with correct reference for factory function
    string name;
    string description;
    Sprite image;

    public Skill(string pName, string pDescription, Sprite pImage)
    {
        name = pName;
        description = pDescription;
        image = pImage;
    }

    public Skill Clone()
    {
        return new Skill(name, description, image);
    }
}
