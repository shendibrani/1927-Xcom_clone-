﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour {

	int _health;

	public PawnEvent OnDeath;
	public delegate void DamageEvent ( Pawn p, int damage);
	public DamageEvent OnDamage;

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
		Debug.Log ("damage");
		if (OnDamage != null) {
			Debug.Log("on damage != null");
			OnDamage(GetComponent<Pawn>(),damage);
		}

		if (health <= 0){ //&& OnDeath != null) {
			Debug.Log("health <= 0");
			if (GetComponent<Pawn>() != null) GetComponent<Pawn>().SetDead();
			OnDeath.Invoke(GetComponent<Pawn>());
		}
	}

	public void Heal (int heal)
	{
		health += heal;
	}


}
