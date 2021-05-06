using UnityEngine;
using System.Collections;

public class LastBOSSDOOR : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if ( FindObjectOfType<BossBehavior >() == null)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
