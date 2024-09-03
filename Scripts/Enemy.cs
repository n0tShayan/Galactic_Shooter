using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Space Rock Settings")]
    public bool isRock = false;


    [Header("Enemy Ship Settings")]

    public List<GameObject> skins;

    public float distanceToPlayer;
    public float playerDetectionRange = 5;
    public float speed = 3;
    public int maxClosingDistance = 3;
    bool isPlayerDetected = false;
    public Transform player;

    public GameObject enemyBullet;

    Vector3 targetPosition;

    float timer = 0;
    public float timeToShoot = 1;



    Vector3 getRandomTarget()
    {

        int randomX = Random.Range(-7, 7);
        int randomY = Random.Range(-4, 4);

        Vector3 randomTarget = new Vector3(randomX, randomY, 0);


        return randomTarget;
    }

    void shootAtPlayer()
    {
        timer += Time.deltaTime;
        if ((timer) >= timeToShoot)
        {
            if (isPlayerDetected)
            {
                Instantiate(enemyBullet, transform.position, transform.rotation);
            }
            timer = 0;
        }
       
    }

    void lookAtTarget()
    {
        transform.up = (targetPosition - transform.position).normalized;
    }

    void setTarget(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }

    void searchForPlayer()
    {
        Vector3 directionVector = player.position - transform.position;
        distanceToPlayer = directionVector.magnitude;

        if (distanceToPlayer < playerDetectionRange)
        {
            setTarget(player.position);
            isPlayerDetected = true;
        }
        else 
        {
            if((targetPosition - transform.position).magnitude < maxClosingDistance )
            {
                setTarget(getRandomTarget());

            }
            isPlayerDetected = false;
        }

    }

    void followTarget()
    {
        if(isPlayerDetected)
        {
            if (distanceToPlayer > maxClosingDistance)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, transform.up * speed, 0.01f);

            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.01f);
            }
        }
        else
        {
            Vector3 directionToCenter = targetPosition - transform.position;
            float distanceToCenterMagnitude = directionToCenter.magnitude;

            if (distanceToCenterMagnitude < maxClosingDistance)
            {

                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.01f);
            }
            else
            {
                rb.velocity = transform.up * speed;

            }
        }

        lookAtTarget();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(skins.Count>0)
        {
            int randomSkin = Random.Range(0, skins.Count);

            GameObject skin = Instantiate(skins[randomSkin], transform.position, transform.rotation);
            skin.transform.parent = gameObject.transform;

            SpriteRenderer sr = skin.GetComponentInChildren<SpriteRenderer>();
            GetComponent<Health>().updateSprite(sr);
        }
        


        rb = GetComponent<Rigidbody2D>();
        targetPosition = Vector3.zero;

        Vector3 directionVector = new Vector3(0, 0, 0) - transform.position;

        transform.up = directionVector.normalized;

        int randomVelocity = Random.Range(5, 10);

        rb.velocity = transform.up * randomVelocity;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }


    private void Update()
    {

        if(!isRock)
        {
            searchForPlayer();
            followTarget();
            shootAtPlayer();
        }
      


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(10);
            if(isRock)
            {
                Destroy(gameObject);

            }
            else
            {
                GetComponent<Health>().TakeDamage(10);
            }
        }
    }

}
