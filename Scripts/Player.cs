using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public int thrust;
    public int maxSpeed = 5;
    bool applyThrust = false;
    

    float tripleBulletDuration = 10f;
    float currentTripleBulletDuration = 0f;

    public bool tripleBullet = false;

    public GameObject bulletPrefab;

    public GameObject tripleBulletPrefab;
    public GameObject singleBulletPrefab;

    public GameObject engineVisual;


    public void setTripleBulletActive(float duration)
    {
        tripleBulletDuration = duration;
        toggleTripleBullet();
    }

    public void toggleTripleBullet()
    {
        if(tripleBullet)
        {
            bulletPrefab = singleBulletPrefab;
            tripleBullet = false;

        }
        else
        {
            bulletPrefab = tripleBulletPrefab;
            tripleBullet = true;

        }
    }

    void updateTriplePowerUpTimer()
    {
        if (tripleBullet)
        {
            currentTripleBulletDuration += Time.deltaTime;
            if (currentTripleBulletDuration > tripleBulletDuration)
            {
                toggleTripleBullet();
                currentTripleBulletDuration = 0;
            }
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

       // rb.AddForce(transform.up * thrust);
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
           if(!engineVisual.activeInHierarchy)
            {
                engineVisual.SetActive(true);
            }
            applyThrust = true;
        }
        else
        {
            if(engineVisual.activeInHierarchy)
            {
                engineVisual.SetActive(false);
            }
            applyThrust = false;
        }


        Vector3 mousePos =  Input.mousePosition;

        Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(mousePos);

        transform.LookAt(worldCoordinates,Vector3.forward);

        transform.rotation = Quaternion.Euler(0, 0, - transform.rotation.eulerAngles.z);

        if(Input.GetMouseButtonDown(0))
        {
            GameObject spawnedBullet =  Instantiate(bulletPrefab, transform.position, transform.rotation);
        }

        updateTriplePowerUpTimer();
    }

    private void FixedUpdate()
    {
        if(applyThrust)
        {
            rb.AddForce(transform.up * thrust);
        }

        if(rb.velocity.magnitude >maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }
}
