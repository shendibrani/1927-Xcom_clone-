using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

[System.Serializable]
public class CoupledSound
{
    public SoundEffects TheSoundEffect;
    public AudioClip[] PlayClips;
}

public enum SoundEffects {
    ASSAULT,
	FLAMETHROWER,
	FREEZE,
	MACHETE,
	SHOTGUN,
	SNIPER
    
}

public class SoundManager : MonoBehaviour
{
	public AudioMixerGroup _output;
    private AudioSource source;

    private AudioSource musicPlayer;
    public AudioClip music;
    public AudioClip menuMusic;

    public static SoundManager instance = null;

    [SerializeField]
    private List<CoupledSound> coupledSoundList;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        source = GetComponent<AudioSource>();

    }

    void Start()
    {
    
        musicPlayer = gameObject.AddComponent<AudioSource>();
		musicPlayer.outputAudioMixerGroup = _output;
        musicPlayer.clip = music;
        musicPlayer.loop = true;
        musicPlayer.playOnAwake = true;
        musicPlayer.Play();
        
    }

    public void PlaySound(SoundEffects se, AudioSource _3DSource)
    {

        AudioClip[] clipArray = coupledSoundList.Find(x => x.TheSoundEffect == se).PlayClips;
        
        int index = Random.Range(0, clipArray.Length);
        //source.clip = clipArray[index];
		_3DSource.clip = clipArray[index];

		if (!_3DSource.isPlaying)
        {
			_3DSource.Play();
        }

     /*   else
        {

            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.clip = clipArray[index];
            newSource.Play();

        }*/

    }

    public void PlaySound(SoundEffects se, bool PlayOneShot)
    {

        AudioClip[] clipArray = coupledSoundList.Find(x => x.TheSoundEffect == se).PlayClips;
        int index = Random.Range(0, clipArray.Length);
        source.clip = clipArray[index];

        if (PlayOneShot) source.PlayOneShot(source.clip);
        else source.Play();


    }

    public void PlaySound(SoundEffects se, bool PlayOneShot, float pVolumeScale)
    {
        AudioClip[] clipArray = coupledSoundList.Find(x => x.TheSoundEffect == se).PlayClips;
        int index = Random.Range(0, clipArray.Length);
        source.clip = clipArray[index];


        if (PlayOneShot) source.PlayOneShot(source.clip, pVolumeScale);
        else source.Play();

    }


}
