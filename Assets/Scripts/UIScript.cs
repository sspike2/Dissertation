using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        videUI = DialogueUI.GetComponent<VideUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlayingMiniGame)
        {
            TimerText.text = (int)GameManager.instance.timePlayed + " / 30";
        }
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



}
