using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenueBehavior : MonoBehaviour {
    public Button start;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Next ()
    {
          SceneManager.LoadScene("PrimeSene");
    }
}
