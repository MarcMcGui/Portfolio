using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ProjectileParent : MonoBehaviour {

	// Public variables
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float width = Camera.main.pixelWidth;
        float edge = Camera.main.ScreenToWorldPoint(new Vector3(width, 0, 0)).x;
        if (transform.position.x > edge)
        {
            GameObject.Destroy(this.gameObject);
        }
        transform.position += Vector3.right / speed;

    }
}