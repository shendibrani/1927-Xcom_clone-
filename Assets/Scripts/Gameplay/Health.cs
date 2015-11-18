using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	int _health;

	public int health { 
		get {
			return _health;
		}
		set {
			if(value > maxHealth){
				_health = maxHealth;
			} else if (value < 0) {
				_health = 0;
			} else {
				_health = value;
			}
		}
	}
	public int maxHealth;

	void Start () 
	{
		health = maxHealth;
	}

	public void Damage (int damage)
	{
		health -= damage;
	}

	public void Heal (int heal)
	{
		health += heal;
	}


}
