using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class EndOfGAme : MonoBehaviour {
    public bool gameOver;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if( gameOver)
        {
            SceneManager.LoadScene(3);
        }
	}
   void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameOver = true;
        }   
    }
}
