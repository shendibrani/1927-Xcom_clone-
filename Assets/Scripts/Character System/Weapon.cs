using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon {

	public string name { get; protected set;}
    public int damage { get; protected set; }
    public int range { get; protected set; }
    public double criticalChance { get; protected set; }
    public double maxHitChance { get; protected set; }
    public double minHitChance { get; protected set; }
    public int actionCost { get; protected set; } //critical will be in decimal format (0.15);
    public List<WeaponEffect> weaponEffects;

//	public Weapon (string pName, int pDamage)
//	{
//		name = pName;
//		damage = pDamage;
//	}
}

public class AssaultRifle : Weapon
{
    public AssaultRifle()
    {
        name = "Assault Rifle";
        damage = 3;
        range = 5;
        criticalChance = 0.09d;
        actionCost = 2;
        weaponEffects = new List<WeaponEffect>();
    }
}

public class PrototypeAssaultRifle : Weapon
{
    public PrototypeAssaultRifle()
    {
        name = "Prototype Assault Rifle";
        damage = 4;
        range = 5;
        criticalChance = 0.09d;
        actionCost = 2;
        weaponEffects = new List<WeaponEffect>();
    }
}

public class SniperRifle : Weapon
{
    public SniperRifle()
    {
        name = "Sniper Rifle";
        damage = 5;
        range = 10;
        criticalChance = 0.15d;
        actionCost = 4;
        weaponEffects = new List<WeaponEffect>();
    }
}

public class PrototypeSniperRifle : Weapon
{
    public PrototypeSniperRifle()
    {
        name = "Prototype Sniper Rifle";
        damage = 6;
        range = 10;
        criticalChance = 0.15d;
        actionCost = 4;
        weaponEffects = new List<WeaponEffect>();
    }
}

public class Cryogun : Weapon
{
    public Cryogun()
    {
        name = "Cryogun";
        damage = 4;
        range = 2;
        actionCost = 3;
        criticalChance = 0.05d;
        weaponEffects = new List<WeaponEffect>();
        weaponEffects.Add(new ApplyChilled());
    }
}



