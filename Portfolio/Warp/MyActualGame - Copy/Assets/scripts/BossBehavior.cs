using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BossBehavior : MonoBehaviour {
    public bool isitOn;
    public bool canSpawnEnemies;
    public bool isStuned;
    public GameObject adds;
    public int count;
    public float health;
    public GameManager game;
    public GameObject myDoor;
    public int enemies;
    public bool isDead;
    public GameObject healthbar;
    public int counter = 250;
    public GameObject lok;
    //public GameObject healthText;
    // Use this initialization
    void Start () {
        isitOn = false;
        enemies = 0;
        health = 100;
        count = 5;
       
	}
	
	// Update is called once per frame
	void Update () {
        if( health < 1 )
        {
            isDead = true;
        }
        canSpawnEnemies = count > 1;
        if (  isitOn )
        {
            healthbar.SetActive(true);
       //     healthText.SetActive(true);
        }
     else
        {
            healthbar.SetActive(false);
         //   healthText.SetActive(false);
        }
        if ( isitOn && !(isDead) && !(isStuned))
        {
            lok.SetActive(true);
            likeDonkeyKong();
            counter = 250;
        }
        if (enemies < 1)
        {
            isStuned = true;
            if (isStuned)
            {

                lok.SetActive(false);
              //  Debug.Log(counter);
                if (counter < 1)
                {
                    isStuned = false;
                    count = 5;
                }
                counter--;

            }
        }
        if (isDead)
        {
            Destroy(gameObject);
            Destroy(myDoor);
        }
       
	}

    public void likeDonkeyKong()
    {
        GameBehavior.isPresent = true;
       // Debug.Log(canSpawnEnemies);
       // Debug.Log(enemies);
        if (canSpawnEnemies )
        {
            Instantiate(adds, transform.position, Quaternion.identity);
            enemies++;
            count--;
        }
        
        
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if ( col.gameObject.tag == "laser" && isStuned )
        {
            health -= 5;
        }
    }
   
    public void respawn()
    {
        health = 100; 
    }
    public void enemyDied()
    {
        enemies--;
    }
    public void stunned()
    {
        
    }
}


