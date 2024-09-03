using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health = 100;
    public Color damageColor;
    public Color normalColor;
    public SpriteRenderer sprite;
    bool isDamaged = false;



    UiManager uimgr;
    public int scoreValue;


    public bool isPlayerHealth;

    public GameObject explosionObject;

    void Start ()
    {
        uimgr = FindObjectOfType<UiManager>();
    }

    IEnumerator changeColor()
    {
        isDamaged = true;
        sprite.color = damageColor;
        yield return new WaitForSeconds(0.05f);
        sprite.color = normalColor;
        isDamaged = false;

    }

    public void updateSprite(SpriteRenderer sr ) // this is for the enemy
    {
        sprite = sr;
    }

    public void damageVisualization()
    {
        if(sprite!=null && !isDamaged)
        {
            sprite.color = damageColor;
            StartCoroutine("changeColor");
        }


        if(gameObject.CompareTag("Player"))
        {
            
        }
    }

    public void updateHealthText()
    {
        uimgr.updateHealthUI(health);

    }

    public void TakeDamage(int amount)
    {
        damageVisualization();



        if (health>0)
        {
            health -= amount;
            if (isPlayerHealth)
            {
                updateHealthText();
            }
        }
        else
        {
            if (isPlayerHealth)
            {
                updateHealthText();
            }
            DestroyCurrent();
        }
    }

    private void DestroyCurrent()
    {
        if (explosionObject != null)
        {
            Destroy(Instantiate(explosionObject, transform.position, transform.rotation), 2);
        }

        if(!isPlayerHealth)
        {
            uimgr.addScore(scoreValue);
        }
        else
        {
            uimgr.showGameOverScreen();
        }
        Destroy(gameObject);
    }

}
