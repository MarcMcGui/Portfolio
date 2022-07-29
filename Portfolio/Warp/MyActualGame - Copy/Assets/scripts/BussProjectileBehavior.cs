using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BussProjectileBehavior : MonoBehaviour
{
    public float speed;
    public BossBehavior boss;
    public PlayerBehavior player;
    public Vector3 point1;
    public Vector3 point2;
    public List<Vector3> points;
    public bool isPatroling;
    public Coroutine patroling;
    public BoxCollider2D me;
    public Coroutine hunting;
    public bool isHunting;
    public int count = 0;
    public bool stunned;
    public Vector3 relativeOpposite;
    int timer = 20;
    public int health;
    // Use this for initialization
    void Start()
    {
        boss = FindObjectOfType<BossBehavior>();
        isPatroling = false;
        player = FindObjectOfType<PlayerBehavior>();
        speed = 10;
        point1 = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
        point2 = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
        points.Add(point1);
        points.Add(point2);
        stunned = false;
        health = 3;
        me = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            transform.Rotate(0, 0, 4);
            relativeOpposite = -((player.transform.position - transform.position)) + transform.position;
         
            if (!stunned)
            {
                //StopCoroutine(patroling);
                if (isHunting)
                {
                    StopCoroutine(hunting);
                    isHunting = false;
                }
                else hunt();
            }

            else
            {

                flee();
                timer--;
                if (timer <= 0)
                {
                    stunned = false;
                    timer = 20;
                }
            }
        }
        else
        {
            transform.position = new Vector3(0, 0, -20);
            if (!me.isTrigger)
            {
                boss.enemyDied();
            }

            me.isTrigger = true;
        }

        // transform.Rotate(transform.position, 1);
    }
    public void hunt()
    {
        if (!isHunting)
        {
            isHunting = true;
            hunting = StartCoroutine(huntNow());
        }
    }

    public IEnumerator huntNow()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void patrol()
    {
        if (!isPatroling)
        {
            isPatroling = true;
            patroling = StartCoroutine(patrolNow());
        }

    }

    public IEnumerator patrolNow()
    {
        // int count = 0;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[count], speed * Time.deltaTime);
            if (transform.position == points[count])
            {
                if (count <= 0)
                {
                    count++;
                }
                else {
                    count = 0;
                }
            }

            yield return null;
        }

    }
    public bool playerIsNear()
    {
        if (player.transform.position.x <= transform.position.x + 10 && player.transform.position.y <= transform.position.y + 10 &&
            player.transform.position.x >= transform.position.x - 10 && player.transform.position.y >= transform.position.y - 10)
        {
            return true;
        }
        else return false;
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            stunned = true;
            player.currentHealth--;
        }
        if (col.gameObject.tag == "laser")
        {
            health--;
        }
    }
    public void flee()
    {
        StopCoroutine(hunting);
      //  StopCoroutine(patroling);
        transform.position = Vector3.MoveTowards(transform.position, relativeOpposite, speed * Time.deltaTime);

    }
    public void respawn()
    {
        transform.position = point1;
        health = 3;
        me.isTrigger = false;
    }
}

