using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	// Public variables
    public float speed = 4.0f;
    public int damage = 1;

	// Internal variables
	private int life;

	// Use this for initialization
	void Start () {
		life = damage;
	}
	
	// Update is called once per frame
	void Update () {
        float width = Camera.main.pixelWidth;
        float edge = Camera.main.ScreenToWorldPoint(new Vector3(width, 0, 0)).x;
        if ( transform.position.x >  edge) {
            GameObject.Destroy(this.gameObject);
        }
        transform.position += Vector3.right / speed;
	}

    private void OnCollisionEnter2D(Collision2D collision) {
		int overflow = 0;
        if ( collision.gameObject.tag == "Fan") {
			overflow = damage - collision.gameObject.GetComponent<FanBehavior> ().life;
            if ( gameObject.tag == "BackUp") {
                collision.gameObject.SendMessage("Freeze");
				collision.gameObject.SendMessage("Hurt", damage);
            }
            else {
				collision.gameObject.SendMessage("Hurt", damage);
            }
			if (overflow > 0) {
				life = 0;
			} else {
				life += overflow;
			}
        }
		if (life <= 0) {
			Destroy (gameObject);
		} else {
			damage = life;
		}
    }
}