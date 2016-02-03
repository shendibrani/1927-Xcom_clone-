using UnityEngine;
using System.Collections;

public class CharacterVisualsSpawn : MonoBehaviour {

	public GameObject[] _Model = new GameObject[0];

	public Material[] _Colors = new Material[0];

	public GameObject[] _Weapons =  new GameObject[0];

	public int _Gender = 0;
	int _Weapon;
	int _AnimCode;

	Pawn _pawn;

	// Use this for initialization
	public void Initialize (Weapons _wep)
	{
		_pawn = GetComponent<Pawn> ();
		PawnAnimationManager _anim = GetComponent<PawnAnimationManager> ();

		if (_wep ==  Weapons.AssaultRifle || _wep == Weapons.PrototypeAssaultRifle)
		{
			_Weapon = 0;
			_AnimCode = 1;
			_anim._ShootSound = SoundEffects.ASSAULT;
		}
		else if(_wep == Weapons.SniperRifle || _wep == Weapons.PrototypeSniperRifle)
		{
			_Weapon = 1;
			_AnimCode = 1;
			_anim._ShootSound = SoundEffects.SNIPER;
		}
		else if(_wep == Weapons.Shotgun || _wep == Weapons.PrototypeShotgun)
		{
			_Weapon = 2;
			_AnimCode = 1;
			_anim._ShootSound = SoundEffects.SHOTGUN;
		}
		else if(_wep == Weapons.Machete || _wep == Weapons.ElectricMachete)
		{
			_Weapon = 3;
			_AnimCode = 3;
			_anim._ShootSound = SoundEffects.MACHETE;
		}
		else if(_wep == Weapons.Cryogun || _wep == Weapons.PrototypeShockGun)
		{
			_Weapon = 4;
			_AnimCode = 2;
			_anim._ShootSound = SoundEffects.SHOTGUN;
		}
		else if(_wep == Weapons.DefaultPistol)
		{
			_Weapon = 5;
			_AnimCode = 0;
			_anim._ShootSound = SoundEffects.SHOTGUN;
		}

		if (_Gender == 2)
		{
			_Gender = Random.Range(0,2);
		}

		GameObject _Hooman = (GameObject) Instantiate(_Model[_Gender]);
		_Hooman.transform.parent = this.transform;
		_Hooman.transform.localPosition = new Vector3 (0, 0.013f, 0);
		_Hooman.transform.localRotation = Quaternion.identity;

		if (_pawn.owner.gameObject.tag == "PlayerChar")
		{
			_Hooman.GetComponentInChildren<Renderer>().material = _Colors[0];
		}
		else
		{
			_Hooman.GetComponentInChildren<Renderer>().material = _Colors[1];
		}

		_Hooman.GetComponent<Animator> ().SetInteger ("Weapon", _AnimCode);

		this.GetComponent<GridNavMeshWrapper> ().SetModelRoot (_Hooman.transform);

		GameObject _WeaponModel = (GameObject)Instantiate (_Weapons [_Weapon]);
		Transform _Hand = _Hooman.GetComponent<HumanSaveHand> ()._HoomanHand.transform;

		_WeaponModel.transform.parent = _Hand;
		_WeaponModel.transform.localPosition = Vector3.zero;
		_WeaponModel.transform.localRotation = Quaternion.identity;
		_WeaponModel.transform.localScale = new Vector3(1,1,1);
	}
}
