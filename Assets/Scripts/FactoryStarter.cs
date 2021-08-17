using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryStarter : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        UIScript.Instance.StartPipeGame();
        // UIScript.Instance.Converse();
        // FindObjectOfType<ThrashTutorialDIrector>().StartTimeLine();
        // transform.DOMove(t.transform.position, .5f);
    }
}
