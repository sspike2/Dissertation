using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using TMPro;
public class VideUI : MonoBehaviour
{


    [SerializeField] GameObject NPC_Container;
    [SerializeField] GameObject player_Container;
    [SerializeField] GameObject dialogue_Container;

    public TextMeshProUGUI NPC_Text;
    public TextMeshProUGUI NPC_Label;

    public TextMeshProUGUI player_Choice1;
    public TextMeshProUGUI player_Choice2;

    public bool animatingText { get; private set; }

    IEnumerator NPC_TextAnimator;
    private void Awake()
    {
        // VD.LoadDialogues();
        VD.LoadState("VIDEDEMOScene1", true);
    }

    public void Interact(VIDE_Assign assign)
    {
        if (!VD.isActive)
        {
            Begin(assign);
        }
        else
        {
            CallNext();
        }

    }


    void Begin(VIDE_Assign dialogue)
    {
        //Let's reset the NPC text variables
        NPC_Text.text = "";
        NPC_Label.text = "";

        //First step is to call BeginDialogue, passing the required VIDE_Assign component 
        //This will store the first Node data in VD.nodeData
        //But before we do so, let's subscribe to certain events that will allow us to easily
        //Handle the node-changes
        // VD.OnActionNode += ActionHandler;
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += EndDialogue;

        VD.BeginDialogue(dialogue); //Begins dialogue, will call the first OnNodeChange

        dialogue_Container.SetActive(true); //Let's make our dialogue container visible
    }



    public void CallNext()
    {
        //Let's not go forward if text is currently being animated, but let's speed it up.
        if (animatingText) { CutTextAnim(); return; }

        // if (!dialoguePaused) //Only if
        // {
        VD.Next(); //We call the next node and populate nodeData with new data. Will fire OnNodeChange.
        // }
        // else
        // {
        //     //Disable item popup and disable pause
        //     if (itemPopUp.activeSelf)
        //     {
        //         dialoguePaused = false;
        //         itemPopUp.SetActive(false);
        //     }
        // }
    }


    void UpdateUI(VD.NodeData data)
    {
        //Reset some variables
        //Destroy the current choices
        player_Choice1.text = "";
        player_Choice2.text = "";
        NPC_Text.text = "";
        NPC_Container.SetActive(false);
        player_Container.SetActive(false);

        //Look for dynamic text change in extraData
        // PostConditions(data);

        //If this new Node is a Player Node, set the player choices offered by the node
        if (data.isPlayer)
        {

            player_Choice1.text = data.comments[0];
            // SetOptions(data.comments);
            if (data.comments.Length > 1)
            {
                player_Choice2.text = data.comments[1];
            }

            //Sets the player container on
            player_Container.SetActive(true);

        }
        else  //If it's an NPC Node, let's just update NPC's text and sprite
        {
            //This coroutine animates the NPC text instead of displaying it all at once
            NPC_TextAnimator = DrawText(data.comments[data.commentIndex], 0.02f);
            StartCoroutine(NPC_TextAnimator);

            //If it has a tag, show it, otherwise let's use the alias we set in the VIDE Assign
            if (data.tag.Length > 0)
                NPC_Label.text = data.tag;
            else
                NPC_Label.text = VD.assigned.alias;

            //Sets the NPC container on
            NPC_Container.SetActive(true);
        }
    }

    void EndDialogue(VD.NodeData data)
    {
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= EndDialogue;
        dialogue_Container.SetActive(false);
        VD.EndDialogue();

        VD.SaveState("VIDEDEMOScene1", true); //Saves VIDE stuff related to EVs and override start nodes
        // QuestChartDemo.SaveProgress(); //saves OUR custom game data
    }


    IEnumerator DrawText(string text, float time)
    {
        animatingText = true;

        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (i != words.Length - 1) word += " ";

            string previousText = NPC_Text.text;

            float lastHeight = NPC_Text.preferredHeight;
            NPC_Text.text += word;
            if (NPC_Text.preferredHeight > lastHeight)
            {
                previousText += System.Environment.NewLine;
            }

            for (int j = 0; j < word.Length; j++)
            {
                NPC_Text.text = previousText + word.Substring(0, j + 1);
                yield return new WaitForSeconds(time);
            }
        }
        NPC_Text.text = text;
        animatingText = false;
    }

    void PostConditions(VD.NodeData data)
    {
        //Don't conduct extra variable actions if we are waiting on a paused action
        if (data.pausedAction) return;

        if (!data.isPlayer) //For player nodes
        {
            //Replace [WORDS]
            // ReplaceWord(data);

            //Checks for extraData that concerns font size (CrazyCap node 2)
            if (data.extraData[data.commentIndex].Contains("fs"))
            {
                int fSize = 14;

                string[] fontSize = data.extraData[data.commentIndex].Split(","[0]);
                int.TryParse(fontSize[1], out fSize);
                NPC_Text.fontSize = fSize;
            }
            else
            {
                NPC_Text.fontSize = 14;
            }
        }
    }

    void CutTextAnim()
    {
        StopCoroutine(NPC_TextAnimator);
        NPC_Text.text = VD.nodeData.comments[VD.nodeData.commentIndex]; //Now just copy full text		
        animatingText = false;
    }






}
