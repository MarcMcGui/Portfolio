using UnityEngine;
using System.Collections;

public class LavaBehavior : MonoBehaviour {
    public PlayerBehavior player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D( Collision2D col )
    {
        if ( col.gameObject.tag == "Player" )
        {
            player.currentHealth = 0; 
        }
    }
}
