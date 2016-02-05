using UnityEngine;
using System.Collections;

public class OptionsUI : MonoBehaviour {

    bool _SSAO;
    bool _CE;
    bool _Bloom;
    bool _Vignette;

	// Use this for initialization
	void Start () {
	
	}

    public void SetSSAO(bool value)
    {
        if (value)
        {
            _SSAO = false;
            PlayerPrefs.SetInt("SSAO", 1);
        }
        else
        {
            _SSAO = true;
            PlayerPrefs.SetInt("SSAO", 0);
        }
    }

    public void SetCE(bool value)
    {
        if (value)
        {
            _CE = false;
            PlayerPrefs.SetInt("CE", 1);
        }
        else
        {
            _CE = true;
            PlayerPrefs.SetInt("CE", 0);
        }
    }

    public void SetBloom(bool value)
    {
        if (value)
        {
            _Bloom = false;
            PlayerPrefs.SetInt("Bloom", 1);
        }
        else
        {
            _Bloom = true;
            PlayerPrefs.SetInt("Bloom", 0);
        }
    }

    public void SetVignette(bool value)
    {
        if (value)
        {
            _Vignette = false;
            PlayerPrefs.SetInt("Vignette", 1);
        }
        else
        {
            _Vignette = true;
            PlayerPrefs.SetInt("Vignette", 0);
        }
    }

	// Update is called once per frame
}
