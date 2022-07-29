using UnityEngine;
using System.Collections;

public class VatBehaviorPresent : MonoBehaviour {
    public bool exists;
    public VialBehaviorPast parent;
	// Use this for initialization
	void Start () {
        exists = true;
        parent = FindObjectOfType<VialBehaviorPast>();
        
	}

    // Update is called once per frame
    void Update()
    {
        exists = parent.exists;
        if ( exists == false)
        {
            transform.position = new Vector3(0, 0, -20); 
        }
    }
}
