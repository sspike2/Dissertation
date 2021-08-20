using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] GameObject riverUIGame;

    PipeManager pipeManager;

    [SerializeField] TextMeshProUGUI pipeGameResult;
    [SerializeField] TextMeshProUGUI powerUpTimerText;
    [SerializeField] TextMeshProUGUI powerUpTypeText;

    [SerializeField] Image riverGameCompelted;


    [SerializeField] Image powerUpIcon;


    [SerializeField] Image powerUpCountDown1, powerUpCountDown2;
    [SerializeField] Sprite magnetIcon, speedIcon, shieldIcon;

    [SerializeField] Slider bgmSlider, sfxSlider;

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

            Pause();
        }
    }

    public void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            PauseMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            PauseMenu.SetActive(true);
        }
    }

    public void ToggleMusic(float val)
    {
        bgmSlider.value = val;
        AudioManager.instance.MusicVolume(val);
    }
    public void ToggleSound(float val)
    {
        sfxSlider.value = val;
        AudioManager.instance.SoundVolume(val);
    }

    public void PowerUpTimer(float timer)
    {
        powerUpCountDown1.fillAmount = timer / 5.0f;
        powerUpCountDown2.fillAmount = timer / 5.0f;
    }

    public void MainMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }


    public void ResetPowerUpText()
    {
        // powerUpTimerText.text = "";
        // powerUpTypeText.text = "";
        powerUpCountDown1.gameObject.SetActive(false);
    }
    public void powerUpType(int type)
    {
        powerUpCountDown1.gameObject.SetActive(true);
        powerUpCountDown1.fillAmount = 1.0f;
        powerUpCountDown2.fillAmount = 1.0f;

        if (type == 0) // speed
        {
            powerUpIcon.sprite = speedIcon;
        }
        else if (type == 1) // immunity
        {
            powerUpIcon.sprite = shieldIcon;
        }//magnet
        else
        {
            powerUpIcon.sprite = magnetIcon;
        }
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

    public void RiverGamePlay()
    {
        ExitRiverGame();
        GameManager.instance.StartRiverMiniGame();
    }

    public void RiverHowToPlay()
    {
        ExitRiverGame();
        FindObjectOfType<ThrashTutorialDIrector>().StartTimeLine();
    }

    public void StartPipeGame()
    {
        PipeUIGame.SetActive(true);
        pipeManager.CheckLevelCompletion();
    }


    public void OpenRiverGame()
    {

        riverGameCompelted.enabled = GameManager.instance.completedRiverMinigame;
        riverUIGame.SetActive(true);
    }

    public void ExitRiverGame()
    {
        riverUIGame.SetActive(false);
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
