using UnityEngine;
using System.Collections;

public class SwitchBehavior : MonoBehaviour {
    public static bool isOn;
    public bool isObstructed;
    public static SpriteRenderer rend;
    public static BoxCollider2D myCollide;
    public static GameObject self;
    public GameObject dor;
    public PlayerBehavior player;
	// Use this for initialization
	void Start () {
        // isObstructed = DebriBehavior.exist;

        player = FindObjectOfType<PlayerBehavior>();
        self = this.gameObject;
        isOn = false;
        rend = GetComponent<SpriteRenderer>();
        myCollide = GetComponent<BoxCollider2D>();
       // door = GetComponentInChildren<GameObject>(); 
	}
    public void changeState()
    {
        isOn = !isOn;
    }
	// Update is called once per frame
	void Update () {
     //  if ( myCollide.IsTouching(player.collide))
       // {
       //     myCollide.isTrigger = true;
       // }
      // else
      //  {
      //     myCollide.isTrigger = false;
      //  }
       // else rend.flipX = false;
        
	}
    void OnTriggerEnter2D(Collider2D col )
    {
        if(col.gameObject.tag == "laser" )
        {
            
            transform.Rotate(0, 180, 0);
            changeState();
              if (!isObstructed)
              {
                  Destroy(dor);
                    //Debug.Log("PUZZLE SOLVED!");
              }
        
            Destroy(col.gameObject);
        }
    }
}
