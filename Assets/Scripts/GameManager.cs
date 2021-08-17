using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField] Spawner riverMiniGameObj;

    [SerializeField] Transform playerRiverPos;


    [SerializeField] GameObject riverCam;
    [SerializeField] GameObject speedBoostCam;

    public PlayerClass player;

    public bool isPlayingMiniGame;

    public float timePlayed;

    Vector3 playerPos;

    [ColorUsage(true, true)]
    [SerializeField]
    Color nonPolluted, semiPolluted, Polluted;

    [SerializeField] Material material;

    int pollutionLevel = 2;

    bool completedRiverMinigame = false, completedPipeGame = false;

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerClass>();
        // StartRiverMiniGame();
    }
    // Start is called before the first frame update
    void Start()
    {

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
            material.DOColor(semiPolluted, "_BaseColor", 1f);
        else
            material.DOColor(nonPolluted, "_BaseColor", 1f);

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
        if (!completedRiverMinigame)
        {
            completedRiverMinigame = true;
            ReducePollutionLevel();
        }
    }

    public void StartRiverMiniGame()
    {
        playerPos = player.transform.position;
        riverMiniGameObj.SetSpawning(true);
        riverCam.SetActive(true);
        player.transform.position = playerRiverPos.position;
        player.currentState = PlayerState.riverMiniGame;
        timePlayed = 0f;
        isPlayingMiniGame = true;
    }



}
