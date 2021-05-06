using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void goToHelp()
    {
        SceneManager.LoadScene(4);
    }


}
