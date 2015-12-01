using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon {

    public Weapons weaponEnum { get; private set; }
	public string name { get; protected set;}
    public int damage { get; protected set; }
    public int range { get; protected set; }
    public double criticalChance { get; protected set; }
    public int actionCost { get; protected set; } //critical will be in decimal format (0.15);
    public List<WeaponEffects> weaponEffects; //read from enum WeaponEffects

    public Weapon() {
    }

    public Weapon(string pName, int pDamage, int pRange, double pCrit, int pAPCost, List<WeaponEffects> pEffects)
    {
        name = pName;
        damage = pDamage;
        range = pRange;
        criticalChance = pCrit;
        actionCost = pAPCost;
        weaponEffects = new List<WeaponEffects>(pEffects);
    }

    public Weapon Clone()
    {
        return new Weapon(name,damage,range,criticalChance,actionCost,weaponEffects);
    }
}

public class WeaponData
{
    public Dictionary<Weapons, Weapon> universalWeaponList;

    public static WeaponData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new WeaponData();
                _instance.LoadFromSave();
            }
            return _instance;
        }
    }

    private static WeaponData _instance;

    private WeaponData() { }

    //load skills from default XML (not related to save)
    public void LoadFromSave()
    {
        universalWeaponList = new Dictionary<Weapons, Weapon>();
        XMLWriter.instance.DeserializeWeapons();
        //universalWeaponList.Add(Weapons.DefaultPistol, new Weapon("Default Pistol", 1, 5, 0, 1, tmpList));
        //universalWeaponList.Add(Weapons.AssaultRifle, new Weapon("Assault Rifle", 3, 5, 0.09d, 2, new List<WeaponEffects>()));
    }
}

public enum Weapons
{
    AssaultRifle,
    PrototypeAssaultRifle,
    SniperRifle,
    PrototypeSniperRifle,
    Shotgun,
    PrototypeShotgun,
    Machete,
    ElectricMachete,
    Cryogun,
    PrototypeShockGun,
    DoublePistol,
    DoublePrototypePistol,
    DefaultPistol
}

public enum WeaponEffects
{
    WeaponChill,
    WeaponStun,
    WeaponEndTurn
}

    /*
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
 */



