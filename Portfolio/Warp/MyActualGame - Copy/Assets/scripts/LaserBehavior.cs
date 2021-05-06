using UnityEngine;
using System.Collections;

public class LaserBehavior : MonoBehaviour {
    public Vector3 direction;
    public float speed;
    public BoxCollider2D col;
    public GameObject des;
    public GameObject swit;
    public bool right;
    public float horiMove;
	// Use this for initialization

   

	void Start () {
        swit = SwitchBehavior.self;
        des = ObjectBehavior.self;
        col = GetComponent<BoxCollider2D>(); 
      //  right = true;
       // direction = transform.right;
       
    }
    void OnCollisionEnter2D(Collision2D coll )
    {
       // Debug.Log("what");
        Destroy(gameObject);


    }

    // Update is called once per frame
    void Update () {


	}
}
