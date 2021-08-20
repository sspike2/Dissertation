using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    CameraScript cameraScript;
    public static GameManager instance;
    [SerializeField] Spawner riverMiniGameObj;

    [SerializeField] Transform playerRiverPos;


    [SerializeField] GameObject riverCam;
    [SerializeField] GameObject speedBoostCam;

    public PlayerClass player;

    public bool isPlayingMiniGame;

    public float timePlayed;

    Vector3 playerPos;
    Quaternion playerRot;

    [ColorUsage(true, true)]
    [SerializeField]
    Color nonPolluted, semiPolluted, Polluted;

    [SerializeField] Material material;

    int pollutionLevel = 2;

    public bool completedRiverMinigame = false, completedPipeGame = false;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerClass>();

        // StartRiverMiniGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraScript = FindObjectOfType<CameraScript>();
        material.SetColor("_BaseColor", Polluted);
    }
    bool col;
    // Update is called once per frame
    void Update()
    {
        if (!isPlayingMiniGame) return;
        timePlayed += Time.deltaTime;

        UIScript.Instance.RiverMiniGameTimer(60 - (int)timePlayed);

        if (timePlayed > 60)
        {
            StopRiverMiniGame();
            UIScript.Instance.DisplayScore();
            isPlayingMiniGame = false;
        }


    }

    public void ReducePollutionLevel()
    {
        pollutionLevel--;

        if (pollutionLevel == 1)
            material.DOColor(semiPolluted, "_BaseColor", 3f);
        else
            material.DOColor(nonPolluted, "_BaseColor", 3f);

    }

    public void CompletePipeGame()
    {
        if (!completedPipeGame)
        {
            pollutionLevel--;
            completedPipeGame = true;
            ReducePollutionLevel();
        }
    }

    public void StopRiverMiniGame()
    {
        player.transform.position = playerPos;
        riverMiniGameObj.SetSpawning(false);
        player.currentState = PlayerState.freeRoam;
        riverCam.SetActive(false);
        // UIScript
        if (!completedRiverMinigame)
        {
            completedRiverMinigame = true;
            ReducePollutionLevel();
        }
    }

    public void StartRiverMiniGame()
    {
        playerPos = player.transform.position;
        playerRot = player.transform.rotation;

        riverMiniGameObj.SetSpawning(true);
        cameraScript.SetBlendTime(0);
        riverCam.SetActive(true);

        player.transform.position = playerRiverPos.position;
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        player.currentState = PlayerState.riverMiniGame;
        timePlayed = 0f;
        isPlayingMiniGame = true;

        StartCoroutine(ResetCamTime());
        IEnumerator ResetCamTime()
        {
            yield return new WaitForSeconds(.1f);
            cameraScript.SetBlendTime(2);
        }
    }



}
