using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenTimer : MonoBehaviour {
    public int count;
	// Use this for initialization
	void Start () {
        count = 100;
	}
	
	// Update is called once per frame
	void Update () {
        count--; 
	    if ( count <= 0)
         {
            SceneManager.LoadScene("Menue", LoadSceneMode.Single);
        }
	}
}
