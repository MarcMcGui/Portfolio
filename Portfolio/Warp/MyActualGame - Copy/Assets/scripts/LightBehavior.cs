using UnityEngine;
using System.Collections;

public class LightBehavior : MonoBehaviour {
    public int threshold;
    public PlayerBehavior player;
    public SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerBehavior>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
	}
	
	// Update is called once per frame
	void Update () {
	    if ( player.vialCounter == threshold )
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        }
        if( player.vialCounter >= 3 )
        {
            Destroy(gameObject);
        }
	}
}
