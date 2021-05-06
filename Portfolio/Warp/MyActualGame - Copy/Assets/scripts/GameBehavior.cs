using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameBehavior : MonoBehaviour {
    public static bool isPresent;
    public bool isLocked; 
    public GameObject[] pastObjects;
    public GameObject[] presentObjects;
	// Use this for initialization
	void Start () {
        isPresent = true;
        pastObjects = GameObject.FindGameObjectsWithTag("Past");
        presentObjects = GameObject.FindGameObjectsWithTag("Present");
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            isPresent = !isPresent;
        }
        if (isPresent)
        {
            if (presentObjects.Length < GameObject.FindGameObjectsWithTag("Present").Length)
            {
                presentObjects = GameObject.FindGameObjectsWithTag("Present");
            }
            for (int i = 0; i <  pastObjects.Length; i++)
            {
                pastObjects[i].SetActive(false);
                
            }
            for(int i = 0; i <presentObjects.Length; i ++)
            {
                presentObjects[i].SetActive(true);
            }
        }
        else
        {
            if (pastObjects.Length < GameObject.FindGameObjectsWithTag("Past").Length)
            {
                pastObjects = GameObject.FindGameObjectsWithTag("Past");
            }
            for (int i = 0; i < presentObjects.Length; i++)
            {
                presentObjects[i].SetActive(false);
                
            }
            for (int i = 0; i < pastObjects.Length; i++)
            {
                pastObjects[i].SetActive(true);

            }
        }
	}
}
