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
        public obstacleStruct(int id, GameObject obj)
        {
            obstacleID = id;
            obstacle = obj;
        }
        public int obstacleID { get; }
        public GameObject obstacle { get; }
    };
    List<obstacleStruct> obstaclePool = new List<obstacleStruct>();

    List<obstacleStruct> starPool = new List<obstacleStruct>();



    // Start is called before the first frame update
    void Start()
    {
        // isSpawning = true;
        defaultTimeBetweenSpawns = timeBetweenSpawns;
        defaultSpeed = obstacleSpeed;



        for (int i = 0; i < 40; i++)
        {
            var index = Random.Range(0, obstacle.Length);
            var obj = Instantiate(obstacle[index], transform.position + new Vector3(0, 0, 500)/*automatically disable it at start*/, Quaternion.identity);
            obstaclePool.Add(new obstacleStruct(index, obj));
        }

        for (int i = 0; i < 10; i++)
        {
            var index = Random.Range(0, StarObjs.Length);
            var obj = Instantiate(StarObjs[index], transform.position + new Vector3(0, 0, 500)/*automatically disable it at start*/, Quaternion.identity);
            starPool.Add(new obstacleStruct(index, obj));
        }
    }

    public obstacleStruct GetObjectFromPool(List<obstacleStruct> list)
    {
        list.Shuffle();
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].obstacle.activeSelf)
            {
                return list[i];
            }
        }
        var index = Random.Range(0, StarObjs.Length);
        var obj = Instantiate(obstacle[index], transform.position, Quaternion.identity);
        var poolObj = new obstacleStruct(index, obj);
        list.Add(poolObj);
        return poolObj;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isSpawning) return;

        spawnDelay += Time.fixedDeltaTime;


        if (spawnDelay > timeBetweenSpawns)
        {
            spawnDelay = 0;
            if (!isSpeedBoosting) starspawnID++;

            if (starspawnID > nextSpawnAfter && !isSpeedBoosting) // star
            {
                var obj = GetObjectFromPool(starPool);
                obj.obstacle.transform.position = transform.position;
                obj.obstacle.SetActive(true);

                nextSpawnAfter = Random.Range(6, 8);
                starspawnID = 0;
            }
            else // normal obs
            {
                var obj = GetObjectFromPool(obstaclePool);

                if (lastIndex == obj.obstacleID)
                {
                    obj = GetObjectFromPool(obstaclePool); // get different obj if same as last one
                }

                lastIndex = obj.obstacleID;

                obj.obstacle.transform.position = transform.position;
                obj.obstacle.SetActive(true);

            }



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


public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}