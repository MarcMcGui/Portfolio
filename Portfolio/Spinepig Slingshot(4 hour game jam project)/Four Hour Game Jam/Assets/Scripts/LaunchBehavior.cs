using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBehavior : MonoBehaviour {
    public bool canLaunch;
    public GameObject player;
    public PlayerController playerReal;
    public bool isFrozen;
    public Rigidbody2D rigid;
    

	// Use this for initialization
	void Start () {
        canLaunch = true;
        isFrozen = true;
        rigid = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
		if (isFrozen)
        {
            rigid.velocity = new Vector3(0, 0, 0);
        }


	}

    private void OnMouseDrag()
    {
        Vector2 mouseLocation = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 realPosition = Camera.main.ScreenToWorldPoint(mouseLocation);
        if ( realPosition.y > -5.0)
        {
            if (realPosition.x > 2)
            {
                realPosition.x = 2;
            }else if (realPosition.x < -2)
            {
                realPosition.x = -2;
            }
             isFrozen = false;
             transform.position = realPosition;
        }
        else
        {
            realPosition.y = -5;
            if (realPosition.x > 2)
            {
                realPosition.x = 2;
            }
            else if (realPosition.x < -2)
            {
                realPosition.x = -2;
            }
             isFrozen = false;
             transform.position = realPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canLaunch)
        {
            if ( Input.GetMouseButton(0) == false )
            {
                Vector3 playerStart = new Vector3(transform.position.x, transform.position.y + 1, 0);
                GameObject playerClone = (GameObject)Instantiate(player, playerStart, Quaternion.identity);
                Rigidbody2D bod = playerClone.GetComponent<Rigidbody2D>();
                bod.velocity = 2 * GetComponent<Rigidbody2D>().velocity;
                playerClone.GetComponent<PlayerController>().inFlight = true;
                canLaunch = false;
            }
            
        }

        
    }
}
