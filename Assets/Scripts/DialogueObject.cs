using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using DG.Tweening;
public class DialogueObject : InteractableObject
{

    public VIDE_Assign dialogue;


    GameObject t;

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
        // transform.DOMove(t.transform.position, .5f);
        UIScript.Instance.OpenRiverGame();
    }
}
