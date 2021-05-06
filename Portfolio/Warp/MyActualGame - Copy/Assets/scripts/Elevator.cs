using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
    public GameObject destination;
    public bool canBeUsed;
	// Use this for initialization
	void Start () {
        canBeUsed = true;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void movedown()
    {
       
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            col.gameObject.transform.position = destination.transform.position;

        }
    }
}
