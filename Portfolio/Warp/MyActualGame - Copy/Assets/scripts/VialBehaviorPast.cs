using UnityEngine;
using System.Collections;

public class VialBehaviorPast : MonoBehaviour {
    public bool exists;
    public PlayerBehavior player;
	// Use this for initialization
	void Start () {
        exists = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.vialCounter >= 2 )
        {
            exists = false;
        }
	    if (exists == false) {
            transform.position = new Vector3(0, 0, - 20);
        }
	}


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "laser")
        {
            exists = false;
        }
    }

}
