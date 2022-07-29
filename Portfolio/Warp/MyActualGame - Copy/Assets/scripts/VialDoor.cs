using UnityEngine;
using System.Collections;

public class VialDoor : MonoBehaviour {
    public PlayerBehavior player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (player.vialCounter > 0 )
        {
          //  Destroy(GetComponentInChildren<GameObject>());
            Destroy(gameObject);
        }
	}
}
