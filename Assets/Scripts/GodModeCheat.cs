using UnityEngine;
using System.Collections;

public class GodModeCheat : MonoBehaviour {

	public int _Damage;
	public bool _Cheat = false;

	// Update is called once per frame
	public void Cheat (Pawn _pawn)
	{
		if (_Cheat)
		{
			Health _H = _pawn.GetComponent<Health> ();
			if (_H != null)
			{
				_H.Damage(_pawn,_Damage);
			}
		}
	}
}
