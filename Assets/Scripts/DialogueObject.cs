using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
public class DialogueObject : InteractableObject
{

    public VIDE_Assign dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<VIDE_Assign>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        // UIScript.Instance.Converse();
        FindObjectOfType<ThrashTutorialDIrector>().StartTimeLine();
    }
}
