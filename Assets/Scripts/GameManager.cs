using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField] Spawner riverMiniGameObj;

    [SerializeField] Transform playerRiverPos;


    [SerializeField] GameObject riverCam;

    PlayerClass player;

    public bool isPlayingMiniGame;

    public float timePlayed;

    Vector3 playerPos;
    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerClass>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayingMiniGame) return;
        timePlayed += Time.deltaTime;

        if (timePlayed > 30)
        {
            StopRiverMiniGame();
            UIScript.Instance.DisplayScore();
            isPlayingMiniGame = false;
        }
    }

    public void StopRiverMiniGame()
    {
        player.transform.position = playerPos;
        riverMiniGameObj.SetSpawning(false);
        player.currentState = PlayerState.freeRoam;
        riverCam.SetActive(false);
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
