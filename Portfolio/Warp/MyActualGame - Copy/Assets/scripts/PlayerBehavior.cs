using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO; 

public class PlayerBehavior : MonoBehaviour {
    //private static PlayerBehavior singleton;
    public ContainerBehavior cont;
    public int lives;
    public bool forceCount;
    public float vialCounter;
    public Rigidbody2D rigid;
    public SpriteRenderer sp;
    public BoxCollider2D collide;
    public PolygonCollider2D topCollider;
    public float vertMove;
    public float horiMove;
    public bool canJump;
    public Camera b;
    public GameObject laser;
    public GameObject healthIcon;
    public bool isRight;
    public float maxHealth;
    public float currentHealth;
    public List<GameObject> healths;
    public Vector3 checkPoint;
    public ZombleBehavior[] zombies;
    public BuzzBehavior[] buzzSaws;
    public CircleCollider2D core;
    public AudioSource jump;
    public AudioClip jumpAudio;
    public Animator anim;
   
    public int[] savedValues;
   
    public float speed = 10f;
    public float jumpSpeed = 2400;
  //  public float jumpCounter;
	// Use this for initialization
	void Start () {
       
            cont = FindObjectOfType<ContainerBehavior>();
            vialCounter = cont.vialCounter;
            maxHealth = cont.maxHealth;
            transform.position = cont.position;
        

       // savedValues = new int[5];
        
      
        zombies = FindObjectsOfType<ZombleBehavior>();
        buzzSaws = FindObjectsOfType<BuzzBehavior>();
       
        core = GetComponent<CircleCollider2D>();
        lives = 5;
        
        FillHealth();
        anim = GetComponent<Animator>();
       // healths = new List<GameObject>();
        checkPoint = transform.position;
        forceCount = true;
        isRight = true;
        canJump = false;
        rigid = GetComponent<Rigidbody2D>();
        collide = GetComponent<BoxCollider2D>();
        topCollider = GetComponent<PolygonCollider2D>();
        sp = GetComponent<SpriteRenderer>();
       // jumpCounter = 20;
	}
	
    void OnCollisionEnter2D(Collision2D coll)
    {
       

        if ( collide.IsTouching(coll.collider))
        {
            //Debug.Log("hey");
            canJump = true;
        }
        else
        {
            
        }
        if( coll.gameObject.tag == "HealthItem")
        {
            maxHealth += 50;
            FillHealth();
        }
        
    }
    
    public void FillHealth()
    {
        currentHealth = maxHealth;
    }

  //  void Awake()
   // {
   //     if (singleton == null)
   //     {
    //        singleton = this;
     //       DontDestroyOnLoad(gameObject);
      //  }
      //  else {
            // if this isn't the first copy, get rid of it
          //  Destroy(gameObject);
      //  }
  //  }
    // Update is called once per frame
    void Update() {

        movement();
        las0r();
       


    }
    void FixedUpdate()
    {
        horiMove = Input.GetAxis("Horizontal");
        transform.Translate((transform.right * horiMove) * (speed * Time.deltaTime), Space.World);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    public void respawn()
    {
        for ( int i = 0; i < zombies.Length; i++)
        {
            zombies[i].respawn();
        }
        for (int i = 0; i < buzzSaws.Length; i++)
        {
            buzzSaws[i].respawn();
        }
        transform.position = checkPoint;
        currentHealth = maxHealth;
        lives-= 1;
        if( lives <= 0 )
        {
            SceneManager.LoadScene("DeathScreen");
        }
    }
    public void movement()
    {
        if (horiMove != 0 && canJump)
        {
            anim.SetBool("isWalking", true);
        }
        else if ( !canJump )
        {
          //  anim.SetBool("isAirBorn", true);
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (currentHealth <= 0)
        {
            respawn();
        }
        vertMove = Input.GetAxis("Vertical");


        if (canJump && Input.GetButtonDown("Jump2"))
        {
            jump.PlayOneShot(jumpAudio, 0.5f ); 
            rigid.AddForce(new Vector2(0, jumpSpeed));
            canJump = false;

        }

        if (horiMove < 0)
        {
            isRight = false;
            sp.flipX = false;

        }
        if (horiMove > 0)
        {
            isRight = true;
            sp.flipX = true;
        }
    }

    public void las0r()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 laserStart;
            if (isRight)
            {
                laserStart = new Vector3(transform.position.x + 1.5f, transform.position.y + 0.5f, 0);
            }
            else
            {
                laserStart = new Vector3(transform.position.x - 1.5f, transform.position.y + 0.5f, 0);
            }
            GameObject laserClone = (GameObject)Instantiate(laser, laserStart, Quaternion.identity);
            Rigidbody2D lazeBod = laserClone.GetComponent<Rigidbody2D>();
            if (isRight)
            {
                lazeBod.velocity = new Vector2(30, 0);
            }
            else
            {
                lazeBod.velocity = new Vector2(-30, 0);
            }

        }
    }
}
