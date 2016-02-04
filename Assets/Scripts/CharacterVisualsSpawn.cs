using UnityEngine;
using System.Collections;

public class CharacterVisualsSpawn : MonoBehaviour 
{
	[SerializeField] bool debug;

	public GameObject[] _Model = new GameObject[0];

	public Material[] _Colors = new Material[0];
	public Material[] _Helmets = new Material[0];

	public GameObject[] _Weapons =  new GameObject[0];

	public GameObject _Helmet;
	public GameObject _GasMask;
	public bool _Gasmask;

	public int _Gender = 0;
	int _Weapon;
	int _AnimCode;

	Pawn _pawn;

	// Use this for initialization
	public void Initialize (Weapons _wep)
	{
		//Debug.Log (_wep);

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

		//Debug.Log ("Gender: " + _Gender);

		GameObject _Hooman = (GameObject) Instantiate(_Model[_Gender]);
		if(debug) Debug.Log("Hooman is null: " + (_Hooman == null));
		_Hooman.transform.parent = this.transform;
		_Hooman.transform.localPosition = new Vector3 (0, 0.013f, 0);
		_Hooman.transform.localRotation = Quaternion.identity;

		_Hooman.GetComponent<Animator> ().SetInteger ("Weapon", _AnimCode);
		_Hooman.GetComponent<Animator> ().SetFloat("StartOffset", Random.value);

		this.GetComponent<GridNavMeshWrapper> ().SetModelRoot (_Hooman.transform);

		GameObject _WeaponModel = (GameObject)Instantiate (_Weapons [_Weapon]);
		if(debug) Debug.Log("WeaponModel is null: " + (_WeaponModel == null));

		Transform _Hand = _Hooman.GetComponent<HumanSaveHand> ()._HoomanHand.transform;
		_WeaponModel.transform.parent = _Hand;
		_WeaponModel.transform.localPosition = Vector3.zero;
		_WeaponModel.transform.localRotation = Quaternion.identity;
		_WeaponModel.transform.localScale = new Vector3(1,1,1);

		Transform _Head = _Hooman.GetComponent<HumanSaveHand> ()._HoomanHead.transform;
		GameObject _HelmetModel = (GameObject)Instantiate (_Helmet);
		_HelmetModel.transform.parent = _Head;
		_HelmetModel.transform.localPosition = Vector3.zero;
		_HelmetModel.transform.localRotation = Quaternion.identity;
		_HelmetModel.transform.localScale = new Vector3(1,1,1);

		if (_Gasmask)
		{
			GameObject _GasMaskModel = (GameObject)Instantiate (_GasMask);
			_GasMaskModel.transform.parent = _Head;
			_GasMaskModel.transform.localPosition = Vector3.zero;
			_GasMaskModel.transform.localRotation = Quaternion.identity;
			_GasMaskModel.transform.localScale = new Vector3(1,1,1);
			
		}

		if (_pawn.owner.gameObject.tag == "PlayerChar")
		{
			_Hooman.GetComponentInChildren<Renderer>().material = _Colors[0];
			_HelmetModel.GetComponentInChildren<Renderer>().material = _Helmets[0];
		}
		else
		{
			_Hooman.GetComponentInChildren<Renderer>().material = _Colors[1];
			_HelmetModel.GetComponentInChildren<Renderer>().material = _Helmets[1];
		}

		//Debug.Log (_WeaponModel.GetComponentsInChildren<Renderer> ().Length);

		GetComponent<VisibleBasedOnLoS> ().models.AddRange (_Hooman.GetComponentsInChildren<Renderer> ());
		GetComponent<VisibleBasedOnLoS> ().models.AddRange (_WeaponModel.GetComponentsInChildren<Renderer> ());
	}
}
