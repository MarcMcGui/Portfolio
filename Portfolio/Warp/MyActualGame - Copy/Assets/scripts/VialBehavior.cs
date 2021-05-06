using UnityEngine;
using System.Collections;

public class VialBehavior : MonoBehaviour {
    public PlayerBehavior player;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.vialCounter++;
            Debug.Log("Blue vial obtained, 2 vials left, CURRENT LEVEL COMPLETE");
            Destroy(gameObject);

        }
    }
}
