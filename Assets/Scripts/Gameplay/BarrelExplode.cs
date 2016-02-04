using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Health))]
public class BarrelExplode : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Health>().OnDeath.AddListener(explode);
	}

    private void explode(Pawn arg0)
    {
        Command cmd = Factory.GetCommand(Commands.Grenade, arg0);
        cmd.freeExec = true;
        cmd.Execute();
    }

}
