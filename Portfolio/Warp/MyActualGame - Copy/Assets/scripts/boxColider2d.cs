using UnityEngine;
using System.Collections;

public class boxColider2d : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if ( FindObjectOfType<BossBehavior>() == null)
        {
            Destroy(gameObject);
        }
	}
}
