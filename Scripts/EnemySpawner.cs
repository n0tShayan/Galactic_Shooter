using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    public List<GameObject> enemies;

    // space rock , ship 
    // 0 , 1 
    // count : 2  - 1 : 0


    float timer = 0;
    public float timeToSpawn = 2;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer>timeToSpawn)
        {
            
            int randomEnemy = Random.Range(0, enemies.Count);

            int randomX = Random.Range(-10, 10);
            int randomY = Random.Range(-5, 5);

            if(randomX>0)
            {
                randomX += 7;
            }
            else
            {
                randomX += -7;
            }


            if(randomY>0)
            {
                randomY += 7;
            }
            else
            {
                randomY += -7;
            }
            //0 , 1 , 2 ... 
            Instantiate(enemies[randomEnemy],new Vector2(randomX,randomY), Quaternion.identity);

            timer = 0;
        }
    }
}
