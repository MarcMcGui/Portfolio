using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lives : MonoBehaviour {
    public PlayerBehavior player;
    public Text tex;
    
	// Use this for initialization
	void Start () {
        tex = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
         tex.text = ("" + player.lives);
	}
}
