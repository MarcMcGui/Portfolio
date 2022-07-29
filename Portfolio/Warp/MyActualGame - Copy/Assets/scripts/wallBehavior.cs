using UnityEngine;
using System.Collections;

public class wallBehavior : MonoBehaviour {
    public bool isWall = true;
    public BoxCollider2D col;
	// Use this for initialization
	void Start () {
        col = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public BoxCollider2D GetCollider()
    {
        return col;
    }
}


