using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class DirectorManager : MonoBehaviour
{
    // Start is called before the first frame update
    PlayableDirector director;
    // [SerializeField] GameObject cut;
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Played;
        director.stopped += Stopped;

        // Invoke("StartTimeLine", 2f);
    }


    void Start()
    {
        // director.Play(director.playableAsset);
    }

    public virtual void Stopped(PlayableDirector obj)
    {
        // Debug.Log("sdfjhgkdfjhgkdfjghdjkf");
        // cut.SetActive(false);
    }

    public virtual void Played(PlayableDirector obj)
    {
        // cut.SetActive(true);
    }

    public virtual void StartTimeLine()
    {
        director.Play();
    }



}
