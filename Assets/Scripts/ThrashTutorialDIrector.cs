using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ThrashTutorialDIrector : DirectorManager
{

    [SerializeField] GameObject cutScene;
    // Start is called before the first frame update
    // IEnumerator Start()
    // {
    // yield return new WaitForSeconds(5f);
    // StartTimeLine();
    // }



    public override void Played(PlayableDirector obj)
    {

        cutScene.SetActive(true);
        base.Played(obj);

    }

    public override void Stopped(PlayableDirector obj)
    {
        cutScene.SetActive(false);
        base.Stopped(obj);
        GameManager.instance.StartRiverMiniGame();
    }

    public override void StartTimeLine()
    {
        base.StartTimeLine();
    }
}
