using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool isSpawning;

    float spawnDelay;

    [SerializeField] GameObject[] obstacle;
    // Start is called before the first frame update
    void Start()
    {
        // isSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;

        spawnDelay += Time.deltaTime;

        if (spawnDelay > 2)
        {
            spawnDelay = 0;
            Instantiate(obstacle[Random.Range(0, obstacle.Length)], transform.position, Quaternion.identity);

        }
    }

    public void SetSpawning(bool state)
    {
        isSpawning = state;
    }

 
}
