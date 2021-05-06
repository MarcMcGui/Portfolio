using UnityEngine;
using System.Collections;

public class DoorExampleBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnCollisionEnter2D(Collision2D col )
    {
        if( col.gameObject.tag == "laser")
        {
            Destroy(gameObject);
        }
    }
}
