using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class UIScript : MonoBehaviour
{

    public GameObject DialogueUI;

    VideUI videUI;
    VIDE_Assign currentDialogue;

    public static UIScript Instance;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] TextMeshProUGUI TimerText;

    [SerializeField] GameObject PipeUIGame;
    [SerializeField] GameObject PauseMenu;
    // [SerializeField] GameObject 

    PipeManager pipeManager;

    [SerializeField] TextMeshProUGUI pipeGameResult;
    [SerializeField] TextMeshProUGUI powerUpTimerText;
    [SerializeField] TextMeshProUGUI powerUpTypeText;


    bool isPaused;
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        videUI = DialogueUI.GetComponent<VideUI>();
        pipeManager = PipeUIGame.GetComponent<PipeManager>();
        InvokeRepeating(nameof(PowerUpTimerColorSwitch), 1, 1);
    }

    void PowerUpTimerColorSwitch()
    {
        powerUpTimerText.DOColor(new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255), .5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ESCAPE 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Debug.Log("fgdfggfdgdfkgldfjgh");
        }
    }

    public void PowerUpTimer(int timer)
    {
        powerUpTimerText.text = timer + "";
    }


    public void ResetPowerUpText()
    {
        powerUpTimerText.text = "";
        powerUpTypeText.text = "";
    }
    public void powerUpType(string type)
    {
        powerUpTypeText.text = type;
    }

    public void RiverMiniGameTimer(int amt)
    {
        TimerText.text = amt + "";
    }
    public void DisplayScore()
    {
        TimerText.text = "";


    }

    public void Converse()
    {
        if (currentDialogue != null)
            videUI.Interact(currentDialogue);
    }

    public void AssignDialogue(VIDE_Assign dialogue)
    {
        currentDialogue = dialogue;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void StartPipeGame()
    {
        PipeUIGame.SetActive(true);
        pipeManager.CheckLevelCompletion();

    }

    public void ExitPipeGame()
    {
        PipeUIGame.SetActive(false);
    }

    public void StopPipeGame(bool hasWon)
    {
        PipeUIGame.SetActive(false);
        if (hasWon)
        {
            pipeGameResult.color = Color.green;
            pipeGameResult.text = "YOU WIN!!!";
        }
        else
        {
            pipeGameResult.color = Color.red;
            pipeGameResult.text = "YOU LOST";
        }
        Invoke(nameof(ResetWinText), 3f);
    }

    void ResetWinText()
    {
        pipeGameResult.text = "";
    }

}
