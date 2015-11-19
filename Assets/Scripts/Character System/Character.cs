using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character {

    int ID;
    string name;
    public int accuracy { get; private set; }
    public int actionPoints { get; private set; }
    public int hitPoints { get; private set; }
    public int level { get; private set; }
    public List<Skill> skillList;
    public int experiencePoints;

}
