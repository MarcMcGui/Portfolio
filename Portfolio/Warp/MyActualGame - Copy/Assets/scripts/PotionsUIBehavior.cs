using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PotionsUIBehavior : MonoBehaviour {
    public Text myText;
    PlayerBehavior player;
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        player = FindObjectOfType<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
        myText.text = ("Vials: " + player.vialCounter + "/3");
	}
}
