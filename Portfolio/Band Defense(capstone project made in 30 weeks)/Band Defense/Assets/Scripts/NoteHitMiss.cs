using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHitMiss : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Animator> ().SetBool ("isHit", false);
		gameObject.GetComponent<Animator> ().SetBool ("isMiss", false);
	}

	void Hit(){
		gameObject.GetComponent<Animator> ().SetBool ("isHit", true);
	}

	void Miss(){
		gameObject.GetComponent<Animator> ().SetBool ("isMiss", true);
	}
}