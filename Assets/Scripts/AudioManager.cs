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
