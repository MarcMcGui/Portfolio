using UnityEngine;
using System.Collections;

public class BossStart : MonoBehaviour {
    public BossBehavior theBossMan;
	// Use this for initialization
	void Start () {
        theBossMan = FindObjectOfType<BossBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.tag == "Player")
        {
            theBossMan.isitOn = true;
            Destroy(gameObject);
        }
        
    }
}
