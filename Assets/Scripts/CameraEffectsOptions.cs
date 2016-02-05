using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraEffectsOptions : MonoBehaviour {

	ScreenSpaceAmbientOcclusion _SSAO;
	ContrastEnhance _CE;
	BloomOptimized _Bloom;
	VignetteAndChromaticAberration _Vignette;

	// Use this for initialization
	void Start () {
		_SSAO = GetComponent<ScreenSpaceAmbientOcclusion> ();
        PlayerPrefs.SetInt("SSAO", 0);
		_CE = GetComponent<ContrastEnhance> ();
        PlayerPrefs.SetInt("CE", 0);
		_Bloom = GetComponent<BloomOptimized> ();
        PlayerPrefs.SetInt("Bloom", 0);
		_Vignette = GetComponent<VignetteAndChromaticAberration> ();
        PlayerPrefs.SetInt("Vignette", 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
        _SSAO.enabled = (PlayerPrefs.GetInt("SSAO") == 0);
        _CE.enabled = (PlayerPrefs.GetInt("CE") == 0);
        _Bloom.enabled = (PlayerPrefs.GetInt("Bloom") == 0);
        _Vignette.enabled = (PlayerPrefs.GetInt("Vignette") == 0);

		if (Input.GetKeyUp(KeyCode.A))
		{
			if (_SSAO.enabled) {
				_SSAO.enabled = false;
                PlayerPrefs.SetInt("SSAO", 1);
			}
			else {
				_SSAO.enabled = true;
                PlayerPrefs.SetInt("SSAO", 0);
			}
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			if (_CE.enabled) {
				_CE.enabled = false;
                PlayerPrefs.SetInt("CE", 1);
			}
			else {
				_CE.enabled = true;
                PlayerPrefs.SetInt("CE", 0);
			}
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			if (_Bloom.enabled) {
				_Bloom.enabled = false;
                PlayerPrefs.SetInt("Bloom", 1);
			}
			else {
				_Bloom.enabled = true;
                PlayerPrefs.SetInt("Bloom", 0);
			}
		}
		if (Input.GetKeyUp(KeyCode.F))
		{
			if (_Vignette.enabled) {
				_Vignette.enabled = false;
                PlayerPrefs.SetInt("Vignette", 1);
			}
			else {
				_Vignette.enabled = true;
                PlayerPrefs.SetInt("Vignette", 0);
			}
		}
	}
}
