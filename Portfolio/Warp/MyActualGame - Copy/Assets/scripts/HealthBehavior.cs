using UnityEngine;
using System.Collections;

public class HealthBehavior : MonoBehaviour {
    public PlayerBehavior player;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnCollisionEnter2D(Collision2D col )
    {
        if (col.gameObject.tag == "Player")
        {
            player.maxHealth += 5;
            player.FillHealth();
            Debug.Log("health increased by 5");
            Destroy(gameObject);
            
        }
    }
}
