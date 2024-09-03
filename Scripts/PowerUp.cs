using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [Header("Shield Power Up")]
    public bool shieldPowerUp;
    public GameObject shieldObject;

    [Header("Health Powerup")]
    public bool healthPowerUp;
    public int healthAmount = 100;

    [Header("Triple Bullet Powerup")]
    public bool tripleBulletPowerUp;
    public float duration = 10f;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (shieldPowerUp)
            {

                Instantiate(shieldObject, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            }

            if (healthPowerUp)
            {
                Health hlth = collision.GetComponent<Health>();
                hlth.health = 100;
                hlth.updateHealthText();
            }
            
            if(tripleBulletPowerUp)
            {
                collision.GetComponent<Player>().setTripleBulletActive(duration);
            }


            Destroy(gameObject);

        }
    }
}
