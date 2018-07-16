using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehavior : MonoBehaviour {
    public PlayerController player;
    public Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		if( ! (FindObjectOfType<PlayerController>() == null) )
        {
            player = FindObjectOfType<PlayerController>();
            text.text = "Score: " + player.score;
            text.fontSize = (int)(100/(1 + Mathf.Pow(2.7f, -0.008f * (player.score - 500))));//L = 100, k = 0.008, x0 = 500
            //L / (1 + e^(-k*(x-xo)))
            text.color = new Color(((1 / (1 + Mathf.Pow(2.7f, -0.008f * (player.score - 500))))),0, 1 - (1 / (1 + Mathf.Pow(2.7f, -0.008f * (player.score - 500))))); 
        }
	}
}
