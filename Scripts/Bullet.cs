using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 10;
    Rigidbody2D rb;
    public int speed = 20;

    public bool isEnemyBullet = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Health>( out Health hlth);

        if(hlth != null )
        {
            hlth.TakeDamage(damage);
            Destroy(gameObject);

        }
        print("yes");

    }

}
