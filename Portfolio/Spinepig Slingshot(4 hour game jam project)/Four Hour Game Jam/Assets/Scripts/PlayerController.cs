using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    float moveSpeed = 5.0f;
    public int score = 0;
    public bool inFlight = false;
    private float gameOverTimer;
    public ParticleSystem particle;
    public Rigidbody2D myBody;

    // Use this for initialization
    void Start () {
        inFlight = true;
        gameOverTimer = 3;
        myBody = GetComponent<Rigidbody2D>();
        myBody.velocity = new Vector2(0, myBody.velocity.y);
        particle = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        float movement = Input.GetAxis("Horizontal") * moveSpeed;
        gameObject.transform.Translate(new Vector3(movement * Time.deltaTime, 0, 0));
        if (this.transform.position.y > score)
        {
            score = (int)this.transform.position.y;
        } else if (gameObject.transform.position.y <= 0)
        {
            inFlight = false;
        }
        if (!inFlight)
        {
            if(gameOverTimer <= 0)
            {
                SceneManager.LoadScene("Menue");
            } 
            gameOverTimer -= Time.deltaTime;
            if (gameObject.transform.position.y > 0)
            {
                inFlight = true;
                gameOverTimer = 3;
            }
        }

        if ( myBody.velocity.y < 6.0f)
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }
            
        }
        else
        {
            if (! particle.isPlaying)
            {
                particle.Play();
            }
            
        }


    }

    private void FixedUpdate()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, -10);
        //Camera.main.transform.position.Set(Camera.main.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
    }

    public void startFlight()
    {
        inFlight = true;
    }
}
