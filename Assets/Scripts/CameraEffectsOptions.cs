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
		_CE = GetComponent<ContrastEnhance> ();
		_Bloom = GetComponent<BloomOptimized> ();
		_Vignette = GetComponent<VignetteAndChromaticAberration> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyUp(KeyCode.A))
		{
			if (_SSAO.enabled) {
				_SSAO.enabled = false;
			}
			else {
				_SSAO.enabled = true;
			}
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			if (_CE.enabled) {
				_CE.enabled = false;
			}
			else {
				_CE.enabled = true;
			}
		}
		if (Input.GetKeyUp(KeyCode.D))
		{
			if (_Bloom.enabled) {
				_Bloom.enabled = false;
			}
			else {
				_Bloom.enabled = true;
			}
		}
		if (Input.GetKeyUp(KeyCode.F))
		{
			if (_Vignette.enabled) {
				_Vignette.enabled = false;
			}
			else {
				_Vignette.enabled = true;
			}
		}
	}
}
