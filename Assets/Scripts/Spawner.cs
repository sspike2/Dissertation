using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool isSpawning;

    [SerializeField] float timeBetweenSpawns;
    public float obstacleSpeed;
    float spawnDelay;

    [Header("PowerUp Stuff")]
    [SerializeField] GameObject speedBoostCam;
    [SerializeField] float timeBetweenPowerupSpawn, powerUpSpeed;
    private float defaultTimeBetweenSpawns, defaultSpeed, speedDuration;

    [SerializeField] GameObject[] obstacle;

    [SerializeField] GameObject[] StarObjs;
    bool isSpeedBoosting;

    int starspawnID = 0;

    int lastIndex = 0;
    int nextSpawnAfter = 4;


    public struct obstacleStruct
    {
        public obstacleStruct(int iD, GameObject obj)
        {
            id = iD;
            obstacle = obj;
        }
        public int id { get; }
        public GameObject obstacle { get; }
    };
    List<obstacleStruct> obj = new List<obstacleStruct>();



    // Start is called before the first frame update
    void Start()
    {
        // isSpawning = true;
        defaultTimeBetweenSpawns = timeBetweenSpawns;
        defaultSpeed = obstacleSpeed;

        for (int i = 0; i < 40; i++)
        {
            var index = Random.Range(0, StarObjs.Length);
            obj.Add(new obstacleStruct(index, Instantiate(obstacle[index], transform.position, Quaternion.identity)));
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isSpawning) return;

        spawnDelay += Time.fixedDeltaTime;


        if (spawnDelay > timeBetweenSpawns)
        {
            spawnDelay = 0;
            if (starspawnID > nextSpawnAfter && !isSpeedBoosting)
            {
                var index = Random.Range(0, StarObjs.Length);

                if (lastIndex == index)
                {
                    if (index == 0)
                        index++;
                    else
                        index--;
                    //                    Random.Range(0, StarObjs.Length);
                }

                lastIndex = index;
                Instantiate(StarObjs[lastIndex], transform.position, Quaternion.identity);
                nextSpawnAfter = Random.Range(4, 8);
                starspawnID = 0;
            }
            else
                Instantiate(obstacle[Random.Range(0, obstacle.Length)], transform.position, Quaternion.identity);

            if (!isSpeedBoosting) starspawnID++;

        }
    }

    public void SetSpawning(bool state)
    {
        isSpawning = state;
    }

    public void SpeedBoost()
    {
        speedBoostCam.SetActive(true);
        timeBetweenSpawns = timeBetweenPowerupSpawn;
        obstacleSpeed = powerUpSpeed;
        Invoke(nameof(ResetSpeedBoost), 5.0f);
        isSpeedBoosting = true;
    }




    public void ResetSpeedBoost()
    {
        speedBoostCam.SetActive(false);
        timeBetweenSpawns = defaultTimeBetweenSpawns;
        obstacleSpeed = defaultSpeed;
        isSpeedBoosting = false;
    }


}
