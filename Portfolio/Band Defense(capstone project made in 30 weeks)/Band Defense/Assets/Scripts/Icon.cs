using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour {

	// Public variables
    public int lane;
    public AudioSource sound;

	// Internal variables
    private bool missed;
    private bool hit;
    private float deathTimer;
    private GameManager gm;
	private BandMember toBuff;
	private BoxCollider2D col;
	private GameObject bar;
	private Animator myAnim;
	private bool listen;
    

	// Use this for initialization
	void Start () {
        deathTimer = 3;
        gm = FindObjectOfType<GameManager>();
        toBuff = gm.getBandByLane(lane);
        col = GetComponent<BoxCollider2D>();
		bar = GameObject.FindGameObjectWithTag("Rhythm Bar");
		myAnim = GetComponent<Animator> ();
		listen = false;
        sound = GetComponent<AudioSource>();
        deathTimer = 2;
        hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= Vector3.right / 15;
		if (listen && (Input.GetAxis ("lane" + (lane + 1)) > 0)) {
			//check to make sure fire rate isn't too high by checking the timer.
			if (toBuff.MaxTimer > (0.25 * toBuff.initialTimer)) {
				toBuff.MaxTimer *= 0.99f;
				var col = toBuff.buffPart.main;
				col.startColor = new Color (col.startColor.color.r, col.startColor.color.g - 0.01f, col.startColor.color.b - 0.01f);
				col.startSize = col.startSize.constant + 0.009f;
				col.startLifetime = col.startLifetime.constant + 0.009f;
			}
			bar.SendMessage ("Hit");
			myAnim.SetBool ("isHit", true);
            hit = true;
		}

        if (missed)
        {
            deathTimer -= Time.deltaTime;
            
        }
        if (deathTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        listen = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        
        missed = true;
		bar.SendMessage("Miss");
        if( !hit )
        {
            sound.PlayDelayed(0.04f);
        }
        

    }

	public void DestroyThis() {
		Destroy (gameObject);
	}
}