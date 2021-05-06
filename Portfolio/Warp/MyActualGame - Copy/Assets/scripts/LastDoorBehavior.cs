using UnityEngine;
using System.Collections;

public class LastDoorBehavior : MonoBehaviour {
    public PlayerBehavior player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	    if ( player.vialCounter >= 3 )
        {
            Destroy(gameObject);
        }
	}
}
