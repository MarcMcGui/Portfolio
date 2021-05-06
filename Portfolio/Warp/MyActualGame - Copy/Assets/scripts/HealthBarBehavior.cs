using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarBehavior : MonoBehaviour {
    public PlayerBehavior player;
    public Scrollbar scroll;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerBehavior>();
        scroll = GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {

        scroll.size = ((float)(player.currentHealth) / (float)(player.maxHealth));
        
	}
}
