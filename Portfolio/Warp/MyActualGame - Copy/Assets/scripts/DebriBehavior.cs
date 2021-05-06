using UnityEngine;
using System.Collections;

public class DebriBehavior : MonoBehaviour {
    public bool exist;
   
    public static GameObject self;
    public ObjectBehavior pastObject;
    public SwitchBehavior swit;
	// Use this for initialization
	void Start () {
        
        self = this.gameObject;
        swit.isObstructed = true;
	}
	
	// Update is called once per frame
	void Update () {
        exist = pastObject.exists;
	    if (!exist)
        {
          
            transform.position = new Vector3(0, 0, -20);
            swit.isObstructed  = false;
        }
	}
}
