using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject options;



    // Start is called before the first frame update
    void Start()
    {

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

    }


    public void MusicChange(float val)
    {

    }
    public void Quit()
    {
        Application.Quit();
    }

}
