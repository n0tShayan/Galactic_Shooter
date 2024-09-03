using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public List<GameObject> powerUps;

    public float spawnTime = 15;
    public float time = 0;

    public float spawnTimeLowerLimit = 5;
    public float spawnTimeUpLimit = 10;

    void calculateSpawnTime()
    {
        float randomSpawnTime = Random.Range(spawnTimeLowerLimit, spawnTimeUpLimit);
        spawnTime = randomSpawnTime;
    }

    private void Start()
    {
        time = 0;
        calculateSpawnTime();
       
    }


    void Update()
    {
        time += Time.deltaTime;


        if(time>spawnTime)
        {
            int randomPowerUp = Random.Range(0, powerUps.Count);
            float randomX = Random.Range(-5, 5);

            Instantiate(powerUps[randomPowerUp],new Vector2(randomX,5),Quaternion.identity);
            time = 0;
            calculateSpawnTime();
        }
    }
}
