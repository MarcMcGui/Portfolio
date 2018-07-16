using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float moveSpeed = 5.0f;
    public int score = 0;
    public bool inFlight = false;
    public ParticleSystem particle;
    public Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        particle = GetComponent<ParticleSystem>();
        particle.Stop();
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float movement = Input.GetAxis("Horizontal") * moveSpeed;
        gameObject.transform.Translate(new Vector3(movement * Time.deltaTime, 0, 0));
        if (this.transform.position.y > score)
        {
            score = (int)this.transform.position.y;
           
        }
        if (rigid.velocity.y > 5.0f)
        {
            Debug.Log("ehat");
            particle.Play();
        }
        else
        {
            particle.Stop();
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
