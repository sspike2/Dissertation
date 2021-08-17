using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Magnet : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("garbage"))
        {
            // bags.Add(other.transform);
            // other.transform.parent = null;
            other.transform.parent.DOMove(GameManager.instance.player.transform.position, .25f);
        }
    }
}