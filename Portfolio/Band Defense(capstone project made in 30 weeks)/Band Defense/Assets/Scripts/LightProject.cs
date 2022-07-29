using System.Collections;
using System.Resources;
using System.Collections.Generic;
using UnityEngine;

public class LightProject : MonoBehaviour {
   
	//Public variables
    public Rigidbody2D rigid;
    public float timer = 1.3f; 

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}

    private void Awake() {
        //as soon as the lighter is spawned launch forward with force vector
        //combined with graviy creates an arching effect
        rigid.AddForce(new Vector3(100, 100, 0));
    }

    // Update is called once per frame
    void Update () { 
        timer -= Time.deltaTime;
		if ( timer <= 0 ) { // Timer in place so it looks like lighter is destroyed after hitting the ground
            Destroy(gameObject);
        }
	}
}