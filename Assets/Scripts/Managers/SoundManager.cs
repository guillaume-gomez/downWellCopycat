using UnityEngine;
using UnityEngine.Audio;
using System.Collections;



public class SoundManager : MonoBehaviour
{
    private const string VFXVolume = "VFX_volume";
    private const string MusicVolume = "Music_volume";

    public AudioMixer vfxMixer;
    public AudioMixer musicMixer;
    public AudioSource vfxSource;                    //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;                    //Drag a reference to the audio source which will play the music.
    public static SoundManager instance = null;        //Allows other scripts to call functions from SoundManager.
    public float lowPitchRange = .95f;                //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    private float musicMixerVolume = 1.0f;
    private float vfxMixerVolume = 1.0f;

    void Awake ()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy (gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad (gameObject);
    }

    void Start()
    {
        
        // UNCOMMENT TO ENABLE SOUND
        musicMixer.SetFloat("Music_volume", -80.0f);
        vfxMixer.SetFloat("VFX_volume", -80.0f);
        // UNCOMMENT TO ENABLE SOUND

    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip, float volume = 1.0f)
    {
        //Set the clip of our vfxSource audio source to the clip passed in as a parameter.
        vfxSource.clip = clip;
        //Play the clip.
        vfxSource.PlayOneShot(clip, volume);
    }

    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx (params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        vfxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        vfxSource.clip = clips[randomIndex];

        //Play the clip.
        vfxSource.Play();
    }

    public void PlayAndMuteMusic(AudioClip clip)
    {
        vfxMixer.GetFloat(VFXVolume, out float currentMusicVolume);
        SetMusicVolume(0.001f);
        PlaySingle(clip);
        StartCoroutine(unMuteMusic(currentMusicVolume, clip.length + 1.0f));

    }

    private IEnumerator unMuteMusic(float oldVolume, float length)
    {
        yield return new WaitForSeconds(length);
        musicMixer.SetFloat(MusicVolume, oldVolume);
    }

    public void SetMusicVolume(float volume)
    {
        // Debug.Log("volume " + Mathf.Log10(volume) * 20);
        musicMixer.SetFloat(MusicVolume, Mathf.Log10(volume) * 20);
    }


    public void SetVFXVolume(float volume)
    {
        // Debug.Log("VFX " + Mathf.Log10(volume) * 20);
        vfxMixer.SetFloat(VFXVolume, Mathf.Log10(volume) * 20);
    }

}