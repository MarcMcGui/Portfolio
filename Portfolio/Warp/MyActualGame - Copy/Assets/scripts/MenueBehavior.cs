using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenueBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void quit()
    {
        Application.Quit();
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
}
