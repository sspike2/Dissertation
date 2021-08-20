using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingObj : MonoBehaviour
{

    Spawner spawner;

    [SerializeField] Transform left, center, right;

    bool isMagnet;

    GameObject disabledObj = null;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();

        left.position += new Vector3(Random.Range(0.0f, -1.5f), 0, 0);
        center.position += new Vector3(Random.Range(-1.5f, 1.5f), 0, 0);
        right.position += new Vector3(Random.Range(0.0f, 1.5f), 0, 0);

    }

    public void SetDisabledObj(GameObject obj)
    {
        disabledObj = obj;
    }


    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf) return;


        // if (!isMagnet)
        // {
        transform.position += new Vector3(0, 0, spawner.obstacleSpeed) * Time.fixedDeltaTime;

        if (transform.position.z >= 500)
        {
            if (disabledObj != null)
            {
                disabledObj.SetActive(true);
                disabledObj = null;
            }
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }
        // }
        // else
        // {          
        // }
    }

}
