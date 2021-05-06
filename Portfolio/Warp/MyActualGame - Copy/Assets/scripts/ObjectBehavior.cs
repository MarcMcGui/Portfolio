using UnityEngine;
using System.Collections;

public class ObjectBehavior : MonoBehaviour {
    public bool isPresent;
    public bool exists = true;
    public static bool isDesctructable;
    public static GameObject self;
	// Use this for initialization
	void Start () {
        self = this.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        isPresent = GameBehavior.isPresent;
        if (!exists) { 
            transform.position = new Vector3(10000, 10000, -10000);
            
        }

    }

    void OnCollisionEnter2D( Collision2D col )
    {
        if( col.gameObject.tag == "laser")
        {
            exists = false;
        }
    }
}
