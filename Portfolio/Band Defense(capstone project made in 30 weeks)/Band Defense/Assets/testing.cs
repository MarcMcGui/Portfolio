using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour {
    float timer;
	// Use this for initialization
	void Start () {
        timer = 100;
	}
	
	// Update is called once per frame
	void Update () {
        timer += 10;
        
        float some = (1000 / (1 + Mathf.Pow(2.7f, -0.008f * (timer - 500))));
        Debug.Log(some);
        Debug.Log("TIEMER:" + timer);
    }
}
