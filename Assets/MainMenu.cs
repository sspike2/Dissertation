using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject options;

    AudioSource BGMaudio;

    public Slider BGMSlider, SFXSlider;

    // Start is called before the first frame update
    void Start()
    {
        BGMaudio = FindObjectOfType<AudioSource>();
        var bgm = PlayerPrefs.GetFloat("BGMVol", 1.0f);
        BGMaudio.volume = bgm;
        BGMSlider.value = bgm;

        var sfx = PlayerPrefs.GetFloat("SFXVol", 1.0f);
        SFXSlider.value = sfx;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenOptions()
    {
        options.SetActive(true);
        options.transform.DOScale(Vector3.one, .25f);
    }

    public void CloseOptions()
    {
        options.transform.DOScale(Vector3.zero, .25f);
        StartCoroutine(Options());
        IEnumerator Options()
        {
            yield return new WaitForSeconds(.25f);
            options.SetActive(false);
        }
    }


    public void SoundChange(float val)
    {
        PlayerPrefs.SetFloat("SFXVol", val);
    }


    public void MusicChange(float val)
    {
        BGMaudio.volume = val;
        PlayerPrefs.SetFloat("BGMVol", val);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
