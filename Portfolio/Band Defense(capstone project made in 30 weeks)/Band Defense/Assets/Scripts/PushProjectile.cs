using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushProjectile : MonoBehaviour {

	// Public variables
    public float timer;
    public float speed = 15;

	// Use this for initialization
	void Start () {
        timer = 3; 
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if( timer <0 )
        {
            GameObject.Destroy(this.gameObject);
        }

        float width = Camera.main.pixelWidth;
        float edge = Camera.main.ScreenToWorldPoint(new Vector3(width, 0, 0)).x;
        transform.position += Vector3.right / speed;
    }
}
