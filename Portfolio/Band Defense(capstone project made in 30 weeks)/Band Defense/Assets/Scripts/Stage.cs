using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour {

	// Public variables
    public float life = 200.0f;
	public Slider healthBar;

	// Use this for initialization
	void Start () {
		healthBar.maxValue = life;
		healthBar.minValue = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if ( life < 1) {
            SceneManager.LoadScene("Death");
        }
		healthBar.value = life;
	}

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "lighter") {
            life -= 20.0f;
            GameObject.Destroy(collision.gameObject);
        }
    }
}