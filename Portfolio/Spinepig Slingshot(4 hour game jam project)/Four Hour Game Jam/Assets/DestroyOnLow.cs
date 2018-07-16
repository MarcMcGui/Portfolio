using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLow : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.y > (transform.position.y + 30))
        {
            GameObject.Destroy(gameObject);
        }
	}
}
