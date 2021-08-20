using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource BGMSource, SFXSource;

    [SerializeField] AudioClip garbageCollect, fishCollect, powerUPCollect;


    public static AudioManager instance;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        var bmg = PlayerPrefs.GetFloat("BGMVol");
        var sfx = PlayerPrefs.GetFloat("SFXVol");

        UIScript.Instance.ToggleMusic(bmg);
        UIScript.Instance.ToggleSound(sfx);

    }

    public void MusicVolume(float val)
    {
        BGMSource.volume = val;
        PlayerPrefs.SetFloat("BGMVol", val);
    }

    public void SoundVolume(float val)
    {
        SFXSource.volume = val;
        PlayerPrefs.SetFloat("SFXVol", val);
    }


    public void PlayGarbageAudio()
    {
        SFXSource.clip = garbageCollect;
        SFXSource.Play();
    }

    public void PlayFishAudio()
    {
        SFXSource.clip = fishCollect;
        SFXSource.Play();
    }
    public void PlayPowerUPAudio()
    {
        SFXSource.clip = powerUPCollect;
        SFXSource.Play();
    }

}
