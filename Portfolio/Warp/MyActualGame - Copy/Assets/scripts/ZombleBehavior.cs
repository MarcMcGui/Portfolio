using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombleBehavior : MonoBehaviour {
    public float speed = 5;
    public float direction = 1;
    public float counter;
    public float health;
    public static SpriteRenderer spr;
    public PlayerBehavior player;
    public BoxCollider2D collider;
    public Vector3 point0;
    public Vector3 point1;
    public Vector3 point2;
    public List<Vector3> points;
    public bool isPatroling;
    public Coroutine patroling;
    public int count = 0;
    // Use this for initialization
    void Start()
    {
        point0 = transform.position;
        transform.Rotate(0, 180, 0);
        health = 3;
        collider = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerBehavior>();
        isPatroling = false;
        speed = 5;
        point1 = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        point2 = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        points.Add(point1);
        points.Add(point2);


    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            transform.position = new Vector3(0, 0, -20);
            collider.isTrigger = true;

        }
        else {
            if (isPatroling)
            {
                StopCoroutine(patroling);
                isPatroling = false;

            }
            else
            {
                patrol();
            }
        }

        // transform.Rotate(transform.position, 1);
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
                transform.Rotate(0, 180, 0);
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
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "laser")
        {
            health -= 1;
            Destroy(coll.gameObject);
            //float temp = direction;
            
          
        }
        if( coll.gameObject.tag == "Player")
        {
            Vector3 relativeOpposite = -((player.transform.position - transform.position)) + transform.position;
            player.currentHealth -= 1;
            player.rigid.AddForce(transform.right * 20);
        }
        if( coll.gameObject.tag == "PlatForm")
        {
            transform.Rotate(0, 180, 0);
        }

    }
   
    public void respawn()
    {
        transform.position = point0;
        health = 3;
        collider.isTrigger = false;
    }
}
