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
            var obj = Instantiate(other.transform.parent.gameObject, other.transform.parent.position, Quaternion.identity);
            other.transform.parent.gameObject.SetActive(false);            
            obj.transform.DOMove(GameManager.instance.player.transform.position, .25f);
        }
    }
}